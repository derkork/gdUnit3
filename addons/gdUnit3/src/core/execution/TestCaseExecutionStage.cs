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
            using (ExecutionContext currentContext = new ExecutionContext(context))
            {
                BeforeTestStage.Execute(currentContext);
                while (!currentContext.Skipped && currentContext.CurrentIteration != 0)
                {
                    currentContext.Test.Execute(currentContext);
                }
                AfterTestStage.Execute(currentContext);
            }
        }
    }
}
