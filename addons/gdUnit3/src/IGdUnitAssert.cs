using System;
using System.ComponentModel;

namespace GdUnit3
{

    /// <summary> Main interface of all GdUnit asserts </summary>
    public interface IGdUnitAssert
    {

        enum EXPECT : int
        {
            [Description("assert expects ends with success")]
            SUCCESS = 0,
            [Description("assert expects ends with errors")]
            FAIL = 1
        }
    }

    /// <summary> Base interface of all GdUnit asserts </summary>
    public interface IGdUnitAssertBase<V> : IGdUnitAssert
    {

        /// <summary>Verifies that the current value is null.</summary>
        IGdUnitAssertBase<V> IsNull();

        /// <summary> Verifies that the current value is not null.</summary>
        IGdUnitAssertBase<V> IsNotNull();

        /// <summary> Verifies that the current value is equal to expected one.
        IGdUnitAssertBase<V> IsEqual(V expected);

        /// <summary> Verifies that the current value is not equal to expected one.</summary>
        IGdUnitAssertBase<V> IsNotEqual(V expected);

        /// <summary></summary>
        IGdUnitAssertBase<V> TestFail();

        /// <summary> Verifies the failure message is equal to expected one.</summary>
        IGdUnitAssertBase<V> HasFailureMessage(string expected);

        /// <summary> Verifies that the failure starts with the given value.</summary>
        IGdUnitAssertBase<V> StartsWithFailureMessage(string value);

        /// <summary> Overrides the default failure message by given custom message.</summary>
        IGdUnitAssertBase<V> OverrideFailureMessage(string message);
    }
}
