using System;

namespace GdUnit3
{
    public class BeforeTestExecutionStage : ExecutionStage<BeforeTestAttribute>
    {
        public BeforeTestExecutionStage(Type type) : base("BeforeTest", type)
        { }

        public override void Execute(ExecutionContext context)
        {
            context.FireTestEvent(TestEvent.BeforeTest(context.TestInstance.ResourcePath, context.TestInstance.Name, context.Test.Name));
            base.Execute(context);
        }
    }
}
