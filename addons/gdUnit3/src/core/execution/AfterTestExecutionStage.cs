using System;

namespace GdUnit3
{
    public class AfterTestExecutionStage : ExecutionStage<AfterTestAttribute>
    {
        public AfterTestExecutionStage(Type type) : base("AfterTest", type)
        { }

        public override void Execute(ExecutionContext context)
        {
            base.Execute(context);
            var statistics = TestEvent.BuildStatistics(0, 0, 0, false, context.Skipped, context.Duration);
            context.FireTestEvent(TestEvent.AfterTest(context.TestInstance.ResourcePath, context.TestInstance.Name, context.Test.Name, statistics));
        }
    }
}
