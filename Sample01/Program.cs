using Microsoft.Extensions.DependencyInjection;
using System;
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
            //var host = serviceProvider.GetService<IWorkflowHost>();
            //host.RegisterWorkflow<HelloWorldWorkflow>();
            //host.Start();
            //host.StartWorkflow("HelloWorld");
            //Console.ReadLine();
            //host.Stop();


            IServiceProvider serviceProvider = ConfigureServices();
            var host = serviceProvider.GetService<IWorkflowHost>();
            var loader = serviceProvider.GetService<IDefinitionLoader>();
            var json = System.IO.File.ReadAllText("workflow.json");
            loader.LoadDefinition(json, Deserializers.Json);
           
            host.OnStepError += Host_OnStepError;
            host.OnLifeCycleEvent += (evt =>
            {
                Console.WriteLine(evt.ToString());
            });
            host.Start();
            host.StartWorkflow("HelloWorld");

            Console.ReadLine();
            host.Stop();
        }

        private static IServiceProvider ConfigureServices()
        {
            ////setup dependency injection
            //IServiceCollection services = new ServiceCollection();
            //services.AddLogging();
            //services.AddWorkflow();
            //services.AddTransient<GoodbyeWorld>();

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
