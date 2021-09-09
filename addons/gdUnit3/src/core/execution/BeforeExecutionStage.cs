using System;

namespace GdUnit3
{
    public class BeforeExecutionStage : ExecutionStage<BeforeAttribute>
    {
        public BeforeExecutionStage(Type type) : base("Before", type)
        { }
    }
}
