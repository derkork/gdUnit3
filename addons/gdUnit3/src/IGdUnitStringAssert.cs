using Godot;
using System;

namespace GdUnit3
{

    /// <summary> An Assertion Tool to verify string values </summary>
    public interface IGdUnitStringAssert : IGdUnitAssertBase<string>
    {
        enum Compare
        {
            EQUAL,
            LESS_THAN,
            LESS_EQUAL,
            GREATER_THAN,
            GREATER_EQUAL,
            BETWEEN_EQUAL,
            NOT_BETWEEN_EQUAL,
        }

        /// <summary> Verifies that the current String is equal to the given one, ignoring case considerations.</summary>
        public IGdUnitStringAssert IsEqualIgnoringCase(string expected);

        /// <summary> Verifies that the current String is not equal to the given one, ignoring case considerations.</summary>
        public IGdUnitStringAssert IsNotEqualIgnoringCase(string expected);

        /// <summary> Verifies that the current String is empty, it has a length of 0.</summary>
        public IGdUnitStringAssert IsEmpty();

        /// <summary> Verifies that the current String is not empty, it has a length of minimum 1.</summary>
        public IGdUnitStringAssert IsNotEmpty();

        /// <summary> Verifies that the current String contains the given String.</summary>
        public IGdUnitStringAssert Contains(string expected);

        /// <summary> Verifies that the current String does not contain the given String.</summary>
        public IGdUnitStringAssert NotContains(string expected);

        /// <summary> Verifies that the current String does not contain the given String, ignoring case considerations.</summary>
        public IGdUnitStringAssert ContainsIgnoringCase(string expected);

        /// <summary> Verifies that the current String does not contain the given String, ignoring case considerations.</summary>
        public IGdUnitStringAssert NotContainsIgnoringCase(string expected);

        /// <summary> Verifies that the current String starts with the given prefix.</summary>
        public IGdUnitStringAssert StartsWith(string expected);

        /// <summary> Verifies that the current String ends with the given suffix.</summary>
        public IGdUnitStringAssert EndsWith(string expected);

        /// <summary> Verifies that the current String has the expected length by used comparator.</summary>
        public IGdUnitStringAssert HasLength(int lenght, Compare comparator = Compare.EQUAL);

    }
}
