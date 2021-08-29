using System;

namespace GdUnit3
{

    /// <summary> Base interface for number assertions.</summary>
    public interface IGdUnitNumberAssert<V> : IGdUnitAssertBase<V>
    {

        /// <summary> Verifies that the current value is less than the given one.</summary>
        public IGdUnitNumberAssert<V> IsLess(V expected);


        /// <summary> Verifies that the current value is less than or equal the given one.</summary>
        public IGdUnitNumberAssert<V> IsLessEqual(V expected);


        /// <summary> Verifies that the current value is greater than the given one.</summary>
        public IGdUnitNumberAssert<V> IsGreater(V expected);


        /// <summary> Verifies that the current value is greater than or equal the given one.</summary>
        public IGdUnitNumberAssert<V> IsGreaterEqual(V expected);


        /// <summary> Verifies that the current value is even.</summary>
        public IGdUnitNumberAssert<V> IsEven();


        /// <summary> Verifies that the current value is odd.</summary>
        public IGdUnitNumberAssert<V> IsOdd();


        /// <summary> Verifies that the current value is negative.</summary>
        public IGdUnitNumberAssert<V> IsNegative();


        /// <summary> Verifies that the current value is not negative.</summary>
        public IGdUnitNumberAssert<V> IsNotNegative();


        /// <summary> Verifies that the current value is equal to zero.</summary>
        public IGdUnitNumberAssert<V> IsZero();


        /// <summary> Verifies that the current value is not equal to zero.</summary>
        public IGdUnitNumberAssert<V> IsNotZero();


        /// <summary> Verifies that the current value is in the given set of values.</summary>
        public IGdUnitNumberAssert<V> IsIn(Array expected);


        /// <summary> Verifies that the current value is not in the given set of values.</summary>
        public IGdUnitNumberAssert<V> IsNotIn(Array expected);


        /// <summary> Verifies that the current value is between the given boundaries (inclusive).</summary>
        public IGdUnitNumberAssert<V> IsBetween(V from, V to);

    }
}
