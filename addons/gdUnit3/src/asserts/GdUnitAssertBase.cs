using Godot;
using System;

namespace GdUnit3
{
    public abstract class GdUnitAssertBase<V> : IGdUnitAssertBase<V>
    {

        protected readonly Godot.Reference _delegator;
        protected readonly object _current;

        protected GdUnitAssertBase(Godot.Reference delegator, object current = null)
        {
            _delegator = delegator;
            _current = current;
        }

        public IGdUnitAssertBase<V> HasFailureMessage(string expected)
        {
            _delegator.Call("has_failure_message", expected);
            return this;
        }

        public IGdUnitAssertBase<V> IsEqual(V expected)
        {
            _delegator.Call("is_equal", expected);
            return this;
        }

        public IGdUnitAssertBase<V> IsNotEqual(V expected)
        {
            _delegator.Call("is_not_equal", expected);
            return this;
        }

        public IGdUnitAssertBase<V> IsNotNull()
        {
            _delegator.Call("is_not_null");
            return this;
        }

        public IGdUnitAssertBase<V> IsNull()
        {
            _delegator.Call("is_null");
            return this;
        }

        public IGdUnitAssertBase<V> OverrideFailureMessage(string message)
        {
            _delegator.Call("override_failure_message", message);
            return this;
        }

        public IGdUnitAssertBase<V> StartsWithFailureMessage(string value)
        {
            _delegator.Call("starts_with_failure_message");
            return this;
        }

        public IGdUnitAssertBase<V> TestFail()
        {
            _delegator.Call("test_fail");
            return this;
        }
    }
}
