using System.Linq;
using System.Reflection;
using System;

namespace GdUnit3
{
    public class BeforeTestExecutionLayer : IExecutionLayer
    {
#nullable enable
        private readonly MethodInfo? _mi;
#nullable disable
        private readonly TestSuite _testSuite;

        public BeforeTestExecutionLayer(TestSuite testSuite)
        {
            _testSuite = testSuite;
            _mi = testSuite.GetType()
               .GetMethods()
               .FirstOrDefault(m =>
               {
                   return m.IsDefined(typeof(BeforeTestAttribute));
               });
        }

        public void Execute()
        {
            if (_mi != null)
            {
                Godot.GD.PrintS("BeforeTestExecutionLayer execute");
                _mi.Invoke(_testSuite, new object[] { });
            }
        }
    }
}
