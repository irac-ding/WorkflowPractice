﻿using System;
using System.Linq;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace MyApp
{
    public class HelloWorld : StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine("HelloWorld");
            return ExecutionResult.Next();
        }
    }
}
