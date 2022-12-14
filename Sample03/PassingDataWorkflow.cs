using System;
using System.Linq;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace MyApp
{
    public class PassingDataWorkflow : IWorkflow<MyDataClass>
    {  
        public void Build(IWorkflowBuilder<MyDataClass> builder)
        {
            builder
                .StartWith(context =>
                {
                    Console.WriteLine("Starting workflow...");
                    return ExecutionResult.Next();
                })
                .Then<AddNumbers>()
                    .Input(step => step.Input1, data => data.Value1)
                    .Input(step => step.Input2, data => data.Value2)
                    .Output(data => data.Answer, step => step.Output)
                .Then<CustomMessage>()
                    .Name("Print custom message")
                    .Input(step => step.Message, data => "The answer is " + data.Answer.ToString())
                .Then(context =>
                    {
                        Console.WriteLine("Workflow complete");
                        return ExecutionResult.Next();
                    });
        }

        public string Id => "PassingDataWorkflow";
            
        public int Version => 1;

    }
}
