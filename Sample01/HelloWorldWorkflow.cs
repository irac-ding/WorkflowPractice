using System;
using System.Linq;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace MyApp
{
    public class HelloWorldWorkflow : IWorkflow
    {
        public void Build(IWorkflowBuilder<object> builder)
        {
            builder
                .UseDefaultErrorBehavior(WorkflowErrorHandling.Suspend)
                .StartWith<HelloWorld>()
                .Then<GoodbyeWorld>();
        }

        public string Id => "HelloWorld";

        public int Version => 1;

    }
}
