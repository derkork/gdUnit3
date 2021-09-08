namespace GdUnit3
{
    public sealed class Executor
    {

        public void execute(TestSuite testSuite)
        {
            Godot.GD.PrintS("Executor start");
            var beforeTest = new BeforeTestExecutionLayer(testSuite);
            var afterTest = new AfterTestExecutionLayer(testSuite);

            foreach (TestCase testCase in CsTools.GetTestCases(testSuite))
            {
                Godot.GD.PrintS("");
                if (testCase.IsSkipped)
                {
                    Godot.GD.PrintS("skip", testCase.Name);
                    testCase.Free();
                    continue;
                }
                beforeTest.Execute();
                new TestCaseExecutionLayer(testCase).Execute();
                afterTest.Execute();
            }

            Godot.GD.PrintS("Executor end");
        }

    }
}
