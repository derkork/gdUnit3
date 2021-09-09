using System;

namespace GdUnit3
{
    public class BeforeTestExecutionStage : ExecutionStage<BeforeTestAttribute>
    {
        public BeforeTestExecutionStage(Type type) : base("BeforeTest", type)
        { }
    }
}
