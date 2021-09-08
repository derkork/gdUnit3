using Godot;
using GdUnit3;


[TestSuite]
public class ExampleTest : TestSuite
{
    public override void Before()
    {
        GD.PrintS("calling Before");
    }

    public override void After()
    {
        GD.PrintS("calling After");
    }

    public override void BeforeTest()
    {
        GD.PrintS("calling BeforeTest");
    }

    public override void AfterTest()
    {
        GD.PrintS("calling AfterTest");
    }

    [TestCase]
    public void TestFoo()
    {
        AssertBool(true).IsEqual(true);
    }

    [TestCase]
    public void TestBar()
    {
        AssertBool(true).IsEqual(true);
    }

}
