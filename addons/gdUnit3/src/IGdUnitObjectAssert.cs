using Godot;
using System;

namespace GdUnit3
{

    /// <summary> An Assertion Tool to verify object values </summary>
    public interface IGdUnitObjectAssert : IGdUnitAssertBase<object>
    {
        // Verifies that the current value is the same as the given one.
        public IGdUnitObjectAssert IsSame(object expected);

        // Verifies that the current value is not the same as the given one.
        public IGdUnitObjectAssert IsNotSame(object expected);

        // Verifies that the current value is an instance of the given type.
        public IGdUnitObjectAssert IsInstanceof<ExpectedType>();

        // Verifies that the current value is not an instance of the given type.
        public IGdUnitObjectAssert IsNotInstanceof<ExpectedType>();

    }
}
