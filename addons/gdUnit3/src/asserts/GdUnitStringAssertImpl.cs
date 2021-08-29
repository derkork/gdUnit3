using Godot;
using System;

namespace GdUnit3
{
    public sealed class GdUnitStringAssertWrapper : GdUnitAssertBase<string>, IGdUnitStringAssert
    {
        private static Godot.GDScript AssertImpl = GD.Load<GDScript>("res://addons/gdUnit3/src/asserts/GdUnitStringAssertImpl.gd");

        public GdUnitStringAssertWrapper(object caller, object current, IGdUnitAssert.EXPECT expectResult)
            : base((Godot.Reference)AssertImpl.New(caller, current, expectResult))
        {
        }

        public IGdUnitStringAssert Contains(string expected)
        {
            _delegator.Call("contains", expected);
            return this;
        }

        public IGdUnitStringAssert ContainsIgnoringCase(string expected)
        {
            _delegator.Call("contains_ignoring_case", expected);
            return this;
        }

        public IGdUnitStringAssert EndsWith(string expected)
        {
            _delegator.Call("ends_with", expected);
            return this;
        }

        public IGdUnitStringAssert HasLength(int lenght, IGdUnitStringAssert.Compare comparator = IGdUnitStringAssert.Compare.EQUAL)
        {
            _delegator.Call("has_length", lenght, comparator);
            return this;
        }

        public IGdUnitStringAssert IsEmpty()
        {
            _delegator.Call("is_empty");
            return this;
        }

        public IGdUnitStringAssert IsEqualIgnoringCase(string expected)
        {
            _delegator.Call("is_equal_ignoring_case", expected);
            return this;
        }

        public IGdUnitStringAssert IsNotEmpty()
        {
            _delegator.Call("is_not_empty");
            return this;
        }

        public IGdUnitStringAssert IsNotEqualIgnoringCase(string expected)
        {
            _delegator.Call("is_not_equal_ignoring_case", expected);
            return this;
        }

        public IGdUnitStringAssert NotContains(string expected)
        {
            _delegator.Call("not_contains", expected);
            return this;
        }

        public IGdUnitStringAssert NotContainsIgnoringCase(string expected)
        {
            _delegator.Call("not_contains_ignoring_case", expected);
            return this;
        }

        public IGdUnitStringAssert StartsWith(string expected)
        {
            _delegator.Call("starts_with", expected);
            return this;
        }

    }
}
