using Godot;
using System;

namespace GdUnit3
{
    public sealed class GdUnitFloatAssertWrapper : GdUnitNumberAssertWrapper<double>, IGdUnitFloatAssert
    {
        private static Godot.GDScript AssertImpl = GD.Load<GDScript>("res://addons/gdUnit3/src/asserts/GdUnitFloatAssertImpl.gd");
        public GdUnitFloatAssertWrapper(object caller, object current, IGdUnitAssert.EXPECT expectResult)
            : base((Godot.Reference)AssertImpl.New(caller, current, expectResult), current)
        {
        }
    }
}
