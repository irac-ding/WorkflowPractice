using System;
using System.IO;
using System.Linq;
using WorkflowCore.Interface;
using WorkflowCore.Models;


namespace VideoFileProcess.Steps
{
    public class CovertToHLS : StepBody
    {
        public string InputVideoFile { get; set; }
        public string ConvertedVideoFile { get; set; }
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine($"CovertToHLS(),InputVideoFile: ${InputVideoFile}");
            ConvertedVideoFile = Path.GetFileNameWithoutExtension(InputVideoFile) + ".hls";
            Console.WriteLine($"CovertToHLS():OutputVideoFile {ConvertedVideoFile}");
            return ExecutionResult.Next();
        }
    }
}
