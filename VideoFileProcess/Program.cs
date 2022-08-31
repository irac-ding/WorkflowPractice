using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VideoFileProcess.WorkFlow;
using WorkflowCore.Interface;
using WorkflowCore.Services.DefinitionStorage;

namespace VideoFileProcess
{
    class Program
    {
        public static void Main(string[] args)
        {
            //IServiceProvider serviceProvider = ConfigureServices();

            ////start the workflow host
            //var host = serviceProvider.GetService<IWorkflowHost>();
            //host.RegisterWorkflow<ConvertToHLSWorkFlow, MyDataClass>();
            //host.RegisterWorkflow<ConvertToMKVWorkFlow, MyDataClass>();
            //host.RegisterWorkflow<ConvertToMp4WorkFlow, MyDataClass>();
            //host.RegisterWorkflow<DownLoadFileAndConvertFileAndUploadWorkFlow, MyDataClass>();
            //host.Start();

            //var initialData1 = new MyDataClass
            //{
            //    FileUrl= "http://10.12.23.123/data//data/VictoriasSecretFashionShow2008.3gp"
            //};

            //host.StartWorkflow("ConvertToHLSWorkFlow", 1, initialData1);
            //host.StartWorkflow("ConvertToMKVWorkFlow", 1, initialData1);
            //host.StartWorkflow("ConvertToMp4WorkFlow", 1, initialData1);

            //host.StartWorkflow("DownLoadFileAndConvertFileAndUploadWorkFlow", 1, initialData1);
            //Console.ReadLine();
            //host.Stop();

            IServiceProvider serviceProvider = ConfigureServices();
            var host = serviceProvider.GetService<IWorkflowHost>();
            var loader = serviceProvider.GetService<IDefinitionLoader>();

            string ConvertToHLSWorkFlowJsonFile = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "json", "ConvertToHLSWorkFlow.json");
            var ConvertToHLSWorkFlowjson = System.IO.File.ReadAllText(ConvertToHLSWorkFlowJsonFile);
            loader.LoadDefinition(ConvertToHLSWorkFlowjson, Deserializers.Json);

            string ConvertToMKVWorkFlowJsonFile = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "json", "ConvertToMKVWorkFlow.json");
            var ConvertToMKVWorkFlowJson = System.IO.File.ReadAllText(ConvertToMKVWorkFlowJsonFile);
            loader.LoadDefinition(ConvertToMKVWorkFlowJson, Deserializers.Json);

            string ConvertToMp4WorkFlowJsonFile = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "json", "ConvertToMp4WorkFlow.json");
            var ConvertToMp4WorkFlowJson = System.IO.File.ReadAllText(ConvertToMp4WorkFlowJsonFile);
            loader.LoadDefinition(ConvertToMp4WorkFlowJson, Deserializers.Json);

            string DownLoadFileAndConvertFileAndUploadWorkFlowJsonFile = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "json", "DownLoadFileAndConvertFileAndUploadWorkFlow.json");
            var DownLoadFileAndConvertFileAndUploadWorkFlowJson = System.IO.File.ReadAllText(DownLoadFileAndConvertFileAndUploadWorkFlowJsonFile);
            loader.LoadDefinition(DownLoadFileAndConvertFileAndUploadWorkFlowJson, Deserializers.Json);

            host.OnStepError += Host_OnStepError;
            host.OnLifeCycleEvent += (evt =>
            {
                Console.WriteLine(evt.ToString());
            });
            host.Start();
            var initialData = new MyDataClass
            {
                FileUrl = "http://10.12.23.123/data//data/VictoriasSecretFashionShow2008.3gp"
            };

            //host.StartWorkflow("ConvertToMKVWorkFlow", 1, initialData);
            //host.StartWorkflow("ConvertToHLSWorkFlow", 1, initialData);
            //host.StartWorkflow("ConvertToMp4WorkFlow", 1, initialData);
            host.StartWorkflow("DownLoadFileAndConvertFileAndUploadWorkFlow", 1, initialData);

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
