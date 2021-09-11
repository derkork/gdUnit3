using System;

namespace GdUnit3
{
    public class BeforeExecutionStage : ExecutionStage<BeforeAttribute>
    {
        public BeforeExecutionStage(Type type) : base("Before", type)
        { }

        public override void Execute(ExecutionContext context)
        {
            base.Execute(context);
            context.FireTestEvent(TestEvent.Before(context.TestInstance.ResourcePath, context.TestInstance.Name, context.TestInstance.TestCaseCount));
        }
    }
}
