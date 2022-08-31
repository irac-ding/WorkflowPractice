using System;
using System.Linq;
using VideoFileProcess.Steps;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace VideoFileProcess.WorkFlow
{
   public class DownLoadFileAndConvertFileAndUploadWorkFlow: IWorkflow<MyDataClass>
    {
        public void Build(IWorkflowBuilder<MyDataClass> builder)
        {
            builder
                .StartWith(context =>
                {
                    Console.WriteLine("Starting workflow...");
                    return ExecutionResult.Next();
                })
                .Then<DownloadVideoFile>()
                    .Input(step => step.VideoFileUrl, data => data.FileUrl)
                    .Output(data => data.VideoFile, step => step.SaveVideoFile)
                 .Then<ConvertToMp4>()
                    .Name("Convert To Mp4")
                    .Input(step => step.InputVideoFile, data => data.VideoFile)
                    .Output(data => data.ConvertedVideoFile, step => step.ConvertedVideoFile)
                .Then<UploadVideoFile>()
                    .Name("Upload file")
                    .Input(step => step.InputVideoFile, data => data.ConvertedVideoFile)
                .Then(context =>
                {
                    Console.WriteLine("Workflow complete");
                    return ExecutionResult.Next();
                });
        }

        public string Id => "DownLoadFileAndConvertFileAndUploadWorkFlow";

        public int Version => 1;
    }
}
