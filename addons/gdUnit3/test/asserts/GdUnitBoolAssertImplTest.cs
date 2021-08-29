using GdUnit3;

[TestSuite]
public class GdUnitBoolAssertImplTest : GdUnitTestSuite
{
    [TestCase]
    public void IsTrue()
    {
        AssertBool(true).IsTrue();
        AssertBool(false, IGdUnitAssert.EXPECT.FAIL).IsTrue()
            .HasFailureMessage("Expecting: 'True' but is 'False'");
    }

    [TestCase]
    public void IsFalse()
    {
        AssertBool(false).IsFalse();
        AssertBool(true, IGdUnitAssert.EXPECT.FAIL).IsFalse()
            .HasFailureMessage("Expecting: 'False' but is 'True'");
    }

    [TestCase]
    public void IsNull()
    {
        AssertBool(true, IGdUnitAssert.EXPECT.FAIL)
            .IsNull()
            .StartsWithFailureMessage("Expecting: 'Null' but was 'True'");
        AssertBool(false, IGdUnitAssert.EXPECT.FAIL)
            .IsNull()
            .StartsWithFailureMessage("Expecting: 'Null' but was 'False'");
    }

    [TestCase]
    public void IsNotNull()
    {
        AssertBool(true).IsNotNull();
        AssertBool(false).IsNotNull();
    }

    [TestCase]
    public void IsEqual()
    {
        AssertBool(true).IsEqual(true);
        AssertBool(false).IsEqual(false);
        AssertBool(true, IGdUnitAssert.EXPECT.FAIL)
            .IsEqual(false)
            .HasFailureMessage("Expecting:\n 'False'\n but was\n 'True'");
    }

    [TestCase]
    public void IsNotEqual()
    {
        AssertBool(true).IsNotEqual(false);
        AssertBool(false).IsNotEqual(true);
        AssertBool(true, IGdUnitAssert.EXPECT.FAIL)
            .IsNotEqual(true)
            .HasFailureMessage("Expecting:\n 'True'\n not equal to\n 'True'");
    }

    [TestCase]
    public void Fluent()
    {
        AssertBool(true).IsTrue()
            .IsEqual(true)
            .IsNotEqual(false)
            .IsNotNull();
    }

    [TestCase]
    public void OverrideFailureMessage()
    {
        AssertBool(true, IGdUnitAssert.EXPECT.FAIL)
            .OverrideFailureMessage("Custom failure message")
            .IsNull()
            .HasFailureMessage("Custom failure message");
    }
}
