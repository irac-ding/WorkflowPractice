
using System;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowCore.Test.Steps
{
    //结束
    public class EndStepBody : StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine("End");
            return ExecutionResult.Next();
        }
    }
}
