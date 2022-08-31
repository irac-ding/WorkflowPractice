using System;
using System.Linq;
using VideoFileProcess.Steps;
using WorkflowCore.Interface;
using WorkflowCore.Models;


namespace VideoFileProcess.WorkFlow
{
  public  class ConvertToMKVWorkFlow : IWorkflow<MyDataClass>
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
                .Then<ConvertToMKV>()
                    .Name("Convert To MKV")
                    .Input(step => step.InputVideoFile, data => data.VideoFile)
                .Then(context =>
                {
                    Console.WriteLine("Workflow complete");
                    return ExecutionResult.Next();
                });
        }

        public string Id => "ConvertToMKVWorkFlow";

        public int Version => 1;

    }
}
