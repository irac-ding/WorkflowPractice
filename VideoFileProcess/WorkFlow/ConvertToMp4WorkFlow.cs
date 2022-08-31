using System;
using System.Linq;
using VideoFileProcess.Steps;
using WorkflowCore.Interface;
using WorkflowCore.Models;


namespace VideoFileProcess.WorkFlow
{
  public  class ConvertToMp4WorkFlow : IWorkflow<MyDataClass>
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
                .Then(context =>
                {
                    Console.WriteLine("Workflow complete");
                    return ExecutionResult.Next();
                });
        }

        public string Id => "ConvertToMp4WorkFlow";

        public int Version => 1;

    }
}
