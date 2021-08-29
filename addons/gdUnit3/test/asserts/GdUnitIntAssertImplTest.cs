using Godot;
using GdUnit3;
using static GdUnit3.IGdUnitAssert.EXPECT;
using static GdUnit3.IGdUnitStringAssert.Compare;

[TestSuite]
public class GdUnitIntAssertImplTest : GdUnitTestSuite
{

    [TestCase]
    public void IsEqual()
    {
        //AssertInt(1).IsLess(2);
        //AssertInt(1).IsEqual(1);
        AssertInt(1).IsEqual(1);

    }

}
