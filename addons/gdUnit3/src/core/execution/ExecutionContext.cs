namespace GdUnit3
{
    public sealed class ExecutionContext
    {

        public ExecutionContext(object testInstance)
        {
            TestInstance = testInstance;
        }

        public object TestInstance
        { get; private set; }


        public IExecutionStage Test
        { get; set; }

        public bool Skipped
        { get; set; }

        private int _iteration;
        public int CurrentIteration
        {
            get => _iteration--;
            set => _iteration = value;
        }
    }

}
