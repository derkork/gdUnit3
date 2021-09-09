namespace GdUnit3
{
    public sealed class Executor
    {

        public void execute(TestSuite testSuite)
        {
            var type = testSuite.GetType();
            var testExecutor = new TestCaseExecutionStage(new BeforeTestExecutionStage(type), new AfterTestExecutionStage(type));

            var context = new ExecutionContext(testSuite);
            new BeforeExecutionStage(type).Execute(context);
            foreach (TestCase testCase in CsTools.GetTestCases(type))
            {
                context.Test = testCase;
                context.CurrentIteration = testCase.Attributes.iterations;
                context.Skipped = testCase.Skipped;

                testExecutor.Execute(context);
            }
            new AfterExecutionStage(type).Execute(context);
        }
    }
}
