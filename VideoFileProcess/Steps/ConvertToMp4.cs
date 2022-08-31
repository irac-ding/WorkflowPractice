using System;
using System.IO;
using System.Linq;
using WorkflowCore.Interface;
using WorkflowCore.Models;


namespace VideoFileProcess.Steps
{
    public class ConvertToMp4 : StepBody
    {
        public string InputVideoFile { get; set; }
        public string ConvertedVideoFile { get; set; }
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine($"ConvertToMp4(),InputVideoFile: ${InputVideoFile}");
            ConvertedVideoFile = Path.GetFileNameWithoutExtension(InputVideoFile) + ".mp4";
            Console.WriteLine($"ConvertToMp4():OutputVideoFile {ConvertedVideoFile}");
            return ExecutionResult.Next();
        }
    }
}
