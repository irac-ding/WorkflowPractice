using System;
using System.IO;
using System.Linq;
using WorkflowCore.Interface;
using WorkflowCore.Models;


namespace VideoFileProcess.Steps
{
    public class UploadVideoFile : StepBody
    {
        public string InputVideoFile { get; set; }
        public string UploadFilePath { get; set; }
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine($"UploadVideoFile(),InputVideoFile:{InputVideoFile}");
            UploadFilePath = $"/data/upload/{Path.GetFileName(InputVideoFile)}";
            Console.WriteLine($"UploadVideoFile(),UploadFilePath:{UploadFilePath}");
            return ExecutionResult.Next();
        }
    }
}
