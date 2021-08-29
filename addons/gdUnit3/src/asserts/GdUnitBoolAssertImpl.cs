using Godot;
using System;

namespace GdUnit3
{
    public sealed class GdUnitBoolAssertWrapper : GdUnitAssertBase<bool>, IGdUnitBoolAssert
    {
        private static Godot.GDScript GdUnitBoolAssertImpl = GD.Load<GDScript>("res://addons/gdUnit3/src/asserts/GdUnitBoolAssertImpl.gd");

        public GdUnitBoolAssertWrapper(object caller, object current, IGdUnitAssert.EXPECT expectResult)
            : base((Godot.Reference)GdUnitBoolAssertImpl.New(caller, current, expectResult))
        {

        }

        public IGdUnitBoolAssert IsFalse()
        {
            _delegator.Call("is_false");
            return this;
        }

        public IGdUnitBoolAssert IsTrue()
        {
            _delegator.Call("is_true");
            return this;
        }

    }
}
