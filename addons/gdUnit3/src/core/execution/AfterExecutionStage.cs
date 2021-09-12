using System;
using System.Linq;

namespace GdUnit3
{
    public class AfterExecutionStage : ExecutionStage<AfterAttribute>
    {
        public AfterExecutionStage(Type type) : base("After", type)
        { }

        public override void Execute(ExecutionContext context)
        {
            base.Execute(context);
            context.FireTestEvent(TestEvent.After(context.TestInstance.ResourcePath, context.TestInstance.Name, context.BuildStatistics()));
        }
    }
}
