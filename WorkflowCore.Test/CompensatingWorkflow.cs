using System;
using WorkflowCore.Interface;
using WorkflowCore.Test.Steps;

namespace WorkflowCore.Test
{

    public class TestModel
    {
        public string Name { get; set; }
        public string Leader1Pass { get; set; }
        public bool Leader2Pass { get; set; }
    }

    class CompensatingWorkflow : IWorkflow<TestModel>
    {
        public string Id => "compensate-sample";
        public int Version => 1;

        public void Build(IWorkflowBuilder<TestModel> builder)
        {
            builder
                .StartWith<NullStepBody>()
                .Then<Task1>()
            .WaitFor("领导1审批", x => "ld1", d => DateTime.Now)
                .Output(d => d.Leader1Pass, s => s.EventData)
                .If(d =>d.Leader1Pass=="y").Do(then =>
                     then.Then<Task2>()
                        )
               .If(d => d.Leader1Pass=="n").Do(then =>
                 then.Then<Task3>()
                 )
                //   .CompensateWith<UndoTask2>()
                // .OnError(Models.WorkflowErrorHandling.Retry, TimeSpan.FromSeconds(5))
                .Then(context => Console.WriteLine("End"));
        }
    }
}
