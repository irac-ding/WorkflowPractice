using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using WorkflowCore.Interface;
using WorkflowCore.Services.DefinitionStorage;

namespace MyApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //IServiceProvider serviceProvider = ConfigureServices();

            ////start the workflow host
            //var host = serviceProvider.GetService<IWorkflowHost>();
            //host.RegisterWorkflow<PassingDataWorkflow, MyDataClass>();
            //host.RegisterWorkflow<PassingDataWorkflow2, Dictionary<string, int>>();
            //host.Start();

            //var initialData = new MyDataClass
            //{
            //    Value1 = 2,
            //    Value2 = 3
            //};

            //host.StartWorkflow("PassingDataWorkflow", 1, initialData);


            //var initialData2 = new Dictionary<string, int>
            //{
            //    ["Value1"] = 7,
            //    ["Value2"] = 2
            //};

            //host.StartWorkflow("PassingDataWorkflow2", 1, initialData2);
            //Console.ReadLine();
            //host.Stop();

            IServiceProvider serviceProvider = ConfigureServices();
            var host = serviceProvider.GetService<IWorkflowHost>();

            var loader = serviceProvider.GetService<IDefinitionLoader>();
            var json = System.IO.File.ReadAllText("PassingDataWorkflow.json");
            loader.LoadDefinition(json, Deserializers.Json);

            host.OnStepError += Host_OnStepError;
            host.OnLifeCycleEvent += (evt =>
            {
                Console.WriteLine(evt.ToString());
            });
            host.Start();
            var initialData = new MyDataClass
            {
                Value1 = 2,
                Value2 = 3
            };

            host.StartWorkflow("PassingDataWorkflow", 1, initialData);

            Console.ReadLine();
            host.Stop();
        }

        private static IServiceProvider ConfigureServices()
        {
            ////setup dependency injection
            //IServiceCollection services = new ServiceCollection();
            //services.AddLogging();
            //services.AddWorkflow();
            //var serviceProvider = services.BuildServiceProvider();

            //return serviceProvider;

            IServiceCollection services = new ServiceCollection();
            services.AddLogging();
            services.AddWorkflow();

            services.AddWorkflowDSL();

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }
        private static void Host_OnLifeCycleEvent(WorkflowCore.Models.LifeCycleEvents.LifeCycleEvent evt)
        {

            Console.WriteLine(evt.ToString());
        }

        private static void Host_OnStepError(WorkflowCore.Models.WorkflowInstance workflow, WorkflowCore.Models.WorkflowStep step, Exception exception)
        {
            System.Console.WriteLine(exception.Message);
        }
    }
}
