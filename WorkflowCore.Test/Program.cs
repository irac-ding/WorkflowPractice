using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WorkflowCore.Interface;
using WorkflowCore.Services.DefinitionStorage;
using Mapster;
using WorkflowCore.Test.Model;
using WorkflowCore.Models.DefinitionStorage.v1;
using WorkflowCore.Test.Steps;
using WorkflowCore.Models;
using Newtonsoft.Json;

namespace WorkflowCore.Test
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var serviceProvider = ConfigureServices();
            var _host = serviceProvider.GetService<IWorkflowHost>();
            var _definitionLoader = serviceProvider.GetService<IDefinitionLoader>();
            _host.Start();

            var step5 = new StepSourceV1()
            {
                Id = Guid.NewGuid().ToString(),
                StepType = $"{typeof(EndStepBody).FullName},{typeof(EndStepBody).Assembly.FullName}",
                Name = "结束",
            };
            var step4 = new StepSourceV1()
            {
                Id = Guid.NewGuid().ToString(),
                StepType = $"{typeof(WorkflowCore.Primitives.If).FullName},{typeof(WorkflowCore.Primitives.If).Assembly.FullName}",
                Name = "领导2意见",
                Inputs = new System.Dynamic.ExpandoObject(),
                NextStepId = step5.Id,
                Do = new List<List<StepSourceV1>>(){new List<StepSourceV1>(){
                  new StepSourceV1(){
                      StepType= $"{typeof(Task2).FullName},{typeof(Task2).Assembly.FullName}",
                       Id = Guid.NewGuid().ToString()
                  }
                }},
            };
            step4.Inputs.TryAdd("Condition", "!Data.Pass");
            step4.Inputs.TryAdd("Condition", $"decimal.Parse(Data.Day.ToString())>5");
            var step3 = new StepSourceV1()
            {
                Id = Guid.NewGuid().ToString(),
                StepType = $"{typeof(WorkflowCore.Primitives.If).FullName},{typeof(WorkflowCore.Primitives.If).Assembly.FullName}",
                Name = "领导1意见",
                Inputs = new System.Dynamic.ExpandoObject(),
                Do = new List<List<StepSourceV1>>(){new List<StepSourceV1>(){
                  new StepSourceV1(){
                      StepType= $"{typeof(Task1).FullName},{typeof(Task1).Assembly.FullName}",
                       Id = Guid.NewGuid().ToString()
                  }
                }},
                NextStepId = step4.Id
            };
            step3.Inputs.TryAdd("Condition", "Data.Pass && decimal.Parse(Data.Day.ToString())<5");

            var step2 = new StepSourceV1()
            {
                Id = Guid.NewGuid().ToString(),
                StepType = $"{typeof(Task3).FullName},{typeof(Task3).Assembly.FullName}",
                Name = "领导3审批",
                NextStepId = step3.Id,
                Outputs = new Dictionary<string, string>() { { "Pass", "Ld3Pass" } }
            };
            var step1 = new StepSourceV1()
            {
                Id = Guid.NewGuid().ToString(),
                StepType = $"{typeof(NullStepBody).FullName},{typeof(NullStepBody).Assembly.FullName}",
                Name = "开始",
                NextStepId = step2.Id
            };

            var steps = new List<StepSourceV1>(){
                 step1,step2,step3,step4,step5
             };
            var model = new DefinitionSourceV1()
            {
                DataType = $"{typeof(Data).FullName},{typeof(Data).Assembly.FullName}",
                DefaultErrorBehavior = WorkflowErrorHandling.Suspend,
                DefaultErrorRetryInterval = null,
                Steps = steps,
                Version = 1,
                Id = Guid.NewGuid().ToString()
            };

            var json = JsonConvert.SerializeObject(model);
            var a = File.ReadAllText("Demo.json");
            // var def = _definitionLoader.LoadDefinition(, Deserializers.Json);
            var def = _definitionLoader.LoadDefinition(a, Deserializers.Json);
            var id = await _host.StartWorkflow(def.Id, new Data() { Day = 6 });//id为当前流程id
            await Task.Run(async () =>
                 {
                     Thread.Sleep(2000);
                     await _host.PublishEvent("Event1", "key1", true);

                 });


            Console.ReadLine();
            _host.Stop();
        }


        private static IServiceProvider ConfigureServices()
        {
            //setup dependency injection
            IServiceCollection services = new ServiceCollection();
            services.AddLogging();
            services.AddWorkflow(x => x.UseMongoDB(@"mongodb://10.12.23.123:27017", "workflow"));
            //services.AddWorkflow(x => x.UseMySQL(@"Server=127.0.0.1;Port=3306;Database=Workflow; User=root;Password=123;Allow User Variables=true;", true, true));
            //services.AddWorkflow(x => x.UsePostgreSQL(@"Server=127.0.0.1;Port=5432;Database=workflow;User Id=postgres;", true, true));
            services.AddWorkflowDSL();
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }



    }
    public class Data
    {
        public bool Pass { get; set; }
        public int Day { get; set; }
    }
}
