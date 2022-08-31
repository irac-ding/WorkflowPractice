
using System;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowCore.Test.Steps
{
    //开始
    public class NullStepBody : StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine("Begin");
            return ExecutionResult.Next();
        }
    }
}
