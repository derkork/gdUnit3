namespace GdUnit3
{
    public sealed class TestCaseExecutionStage : IExecutionStage
    {
        public TestCaseExecutionStage(IExecutionStage before, IExecutionStage after)
        {
            Before = before;
            After = after;
        }

        public string StageName()
        {
            return "TestCaseIteration";
        }

        private IExecutionStage Before
        { get; set; }
        private IExecutionStage After
        { get; set; }

        public void Execute(ExecutionContext context)
        {
            if (context.Skipped)
            { return; }

            Before.Execute(context);
            while (context.CurrentIteration != 0)
            {
                context.Test.Execute(context);
            }
            After.Execute(context);
        }
    }
}
