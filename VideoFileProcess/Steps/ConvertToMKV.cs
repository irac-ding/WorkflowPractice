using System;
using System.IO;
using System.Linq;
using WorkflowCore.Interface;
using WorkflowCore.Models;


namespace VideoFileProcess.Steps
{
   public class ConvertToMKV : StepBody
    {
        public string InputVideoFile { get; set; }
        public string ConvertedVideoFile { get; set; }
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine($"ConvertToMKV(),InputVideoFile: ${InputVideoFile}");
            ConvertedVideoFile = Path.GetFileNameWithoutExtension(InputVideoFile) + ".mkv";
            Console.WriteLine($"ConvertToMKV():OutputVideoFile {ConvertedVideoFile}");
            return ExecutionResult.Next();
        }
    }
}
