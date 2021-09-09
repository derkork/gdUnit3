using System;

namespace GdUnit3
{
    public class AfterTestExecutionStage : ExecutionStage<AfterTestAttribute>
    {
        public AfterTestExecutionStage(Type type) : base("AfterTest", type)
        { }
    }
}
