using System;
using System.IO;
using System.Linq;
using WorkflowCore.Interface;
using WorkflowCore.Models;


namespace VideoFileProcess.Steps
{
    public class DownloadVideoFile : StepBody
    {
        public string VideoFileUrl { get; set; }
        public string SaveVideoFile { get; set; }
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine($"DownloadVideoFile(), VideoFileUrl: {VideoFileUrl}");
            SaveVideoFile = $"/data/FileDownLoad/{Path.GetFileName(VideoFileUrl)}";
            Console.WriteLine($"DownloadVideoFile(), SaveVideoFile: {SaveVideoFile}");
            return ExecutionResult.Next();
        }
    }
}
