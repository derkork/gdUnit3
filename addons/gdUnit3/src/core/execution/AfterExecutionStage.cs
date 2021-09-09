using System;

namespace GdUnit3
{
    public class AfterExecutionStage : ExecutionStage<AfterAttribute>
    {
        public AfterExecutionStage(Type type) : base("After", type)
        { }
    }
}
