using Godot;
using System;

namespace GdUnit3
{
    public class GdUnitNumberAssertWrapper<V> : GdUnitAssertBase<V>, IGdUnitNumberAssert<V>
    {
        public GdUnitNumberAssertWrapper(Godot.Reference delegator, object current)
            : base(delegator, current)
        {
        }

        public IGdUnitNumberAssert<V> IsBetween(V from, V to)
        {
            _delegator.Call("is_between", from, to);
            return this;
        }

        public IGdUnitNumberAssert<V> IsEven()
        {
            _delegator.Call("is_even");
            return this;
        }

        public IGdUnitNumberAssert<V> IsGreater(V expected)
        {
            _delegator.Call("is_greater");
            return this;
        }

        public IGdUnitNumberAssert<V> IsGreaterEqual(V expected)
        {
            _delegator.Call("is_greater_equal");
            return this;

        }

        public IGdUnitNumberAssert<V> IsIn(Array expected)
        {
            _delegator.Call("is_in", expected);
            return this;

        }

        public IGdUnitNumberAssert<V> IsLess(V expected)
        {
            _delegator.Call("is_less", expected);
            return this;

        }

        public IGdUnitNumberAssert<V> IsLessEqual(V expected)
        {
            _delegator.Call("is_less_equal", expected);
            return this;

        }

        public IGdUnitNumberAssert<V> IsNegative()
        {
            _delegator.Call("is_negative");
            return this;

        }

        public IGdUnitNumberAssert<V> IsNotIn(Array expected)
        {
            _delegator.Call("is_not_in", expected);
            return this;

        }

        public IGdUnitNumberAssert<V> IsNotNegative()
        {
            _delegator.Call("is_not_negative");
            return this;

        }

        public IGdUnitNumberAssert<V> IsNotZero()
        {
            _delegator.Call("is_not_zero");
            return this;

        }

        public IGdUnitNumberAssert<V> IsOdd()
        {
            _delegator.Call("is_odd");
            return this;

        }

        public IGdUnitNumberAssert<V> IsZero()
        {
            _delegator.Call("is_zero");
            return this;

        }
    }
}
