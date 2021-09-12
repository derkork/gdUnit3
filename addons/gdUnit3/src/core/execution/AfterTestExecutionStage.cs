using System;
using System.Linq;

namespace GdUnit3
{
    public class AfterTestExecutionStage : ExecutionStage<AfterTestAttribute>
    {
        public AfterTestExecutionStage(Type type) : base("AfterTest", type)
        { }

        public override void Execute(ExecutionContext context)
        {
            base.Execute(context);
            var testEvent = TestEvent.AfterTest(context.TestInstance.ResourcePath,
                context.TestInstance.Name,
                context.Test.Name,
                context.BuildStatistics(),
                context.ReportCollector.Reports);
            context.FireTestEvent(testEvent);
        }
    }
}
