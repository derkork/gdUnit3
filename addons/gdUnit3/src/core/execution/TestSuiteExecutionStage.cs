using System;

namespace GdUnit3
{
    public sealed class TestSuiteExecutionStage : IExecutionStage
    {
        public TestSuiteExecutionStage(Type type)
        {
            BeforeStage = new BeforeExecutionStage(type);
            AfterStage = new AfterExecutionStage(type);
            TestCaseStage = new TestCaseExecutionStage(type);
        }

        public string StageName() => "TestSuite";

        private IExecutionStage BeforeStage
        { get; set; }
        private IExecutionStage AfterStage
        { get; set; }

        private IExecutionStage TestCaseStage
        { get; set; }

        public void Execute(ExecutionContext context)
        {
            using (ExecutionContext currentContext = new ExecutionContext(context))
            {
                BeforeStage.Execute(currentContext);
                foreach (TestCase testCase in CsTools.GetTestCases(context.TestInstance.GetType()))
                {
                    TestCaseStage.Execute(new ExecutionContext(currentContext, testCase));
                }
                AfterStage.Execute(currentContext);
            }
        }
    }
}
