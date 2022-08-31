using System;
using System.Linq;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace MyApp
{
    public class GoodbyeWorld : StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine("Goodbye world");
            return ExecutionResult.Next();
        }
    }
}
