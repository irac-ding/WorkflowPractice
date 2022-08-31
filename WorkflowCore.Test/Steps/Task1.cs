using System;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowCore.Test.Steps
{
    public class Task1 : StepBody
    {
        // public bool d1pass { get; set; }
        public override ExecutionResult Run(IStepExecutionContext context)
        {

            Console.WriteLine("领导1看也不看！");
            return ExecutionResult.Next();


        }
    }
}
