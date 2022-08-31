using System;
using System.Threading;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowCore.Test.Steps
{
    public class Task3 : StepBody
    {
        public bool Ld3Pass { get; set; } = false;
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            if (!context.ExecutionPointer.EventPublished)
            {
                Console.WriteLine("领导3先审批");
                return ExecutionResult.WaitForEvent("Event1", "key1", DateTime.Now);
            }
            else
            {
                Ld3Pass = (bool)context.ExecutionPointer.EventData;
                Console.WriteLine("领导3审批了！");
                return ExecutionResult.Next();
            }
            //  return ExecutionResult.Next();
        }
    }
}
