using Godot;
using System;

namespace GdUnit3
{
    public sealed class GdUnitIntAssertWrapper : GdUnitNumberAssertWrapper<int>, IGdUnitIntAssert
    {
        private static Godot.GDScript AssertImpl = GD.Load<GDScript>("res://addons/gdUnit3/src/asserts/GdUnitIntAssertImpl.gd");

        public GdUnitIntAssertWrapper(object caller, object current, IGdUnitAssert.EXPECT expectResult)
            : base((Godot.Reference)AssertImpl.New(caller, current, expectResult), current)
        {
        }
    }
}
