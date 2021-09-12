using System;

namespace GdUnit3
{
    public sealed class TestCaseExecutionStage : IExecutionStage
    {
        public TestCaseExecutionStage(Type type)
        {
            BeforeTestStage = new BeforeTestExecutionStage(type);
            AfterTestStage = new AfterTestExecutionStage(type);
        }

        public string StageName() => "TestCases";

        private IExecutionStage BeforeTestStage
        { get; set; }
        private IExecutionStage AfterTestStage
        { get; set; }

        public void Execute(ExecutionContext context)
        {
            BeforeTestStage.Execute(context);
            while (!context.IsSkipped() && context.CurrentIteration != 0)
            {
                context.Test.Execute(context);
            }
            AfterTestStage.Execute(context);
        }
    }
}
