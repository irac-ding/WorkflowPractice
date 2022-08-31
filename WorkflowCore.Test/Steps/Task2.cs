using System;
using System.Threading;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowCore.Test.Steps
{
    public class Task2 : StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {

            Console.WriteLine("领导2审批");

            return ExecutionResult.Next();
        }
    }
}
