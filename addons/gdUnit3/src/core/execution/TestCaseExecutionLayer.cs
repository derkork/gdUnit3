namespace GdUnit3
{
    public sealed class TestCaseExecutionLayer : IExecutionLayer
    {
        private readonly TestCase _testCase;
        private int _iterations;

        public TestCaseExecutionLayer(TestCase testCase)
        {
            _testCase = testCase;
            _iterations = testCase.Attributes.iterations;
        }
        public void Execute()
        {
            while (_iterations-- != 0)
            {
                _testCase.execute();
            }
            _testCase.Free();
        }
    }
}
