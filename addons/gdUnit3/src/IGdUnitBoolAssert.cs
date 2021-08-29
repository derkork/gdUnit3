using Godot;
using System;

namespace GdUnit3
{

    /// <summary> An Assertion Tool to verify boolean values </summary>
    public interface IGdUnitBoolAssert : IGdUnitAssertBase<bool>
    {

        /// <summary> Verifies that the current value is true.</summary>
        IGdUnitBoolAssert IsTrue();

        /// <summary> Verifies that the current value is false.</summary>
        IGdUnitBoolAssert IsFalse();

    }
}