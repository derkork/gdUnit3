using System.Linq;
using System.Reflection;
using System;

namespace GdUnit3
{
    public class AfterTestExecutionLayer : IExecutionLayer
    {
#nullable enable
        private readonly MethodInfo? _mi;
#nullable disable
        private readonly TestSuite _testSuite;

        public AfterTestExecutionLayer(TestSuite testSuite)
        {
            _testSuite = testSuite;
            _mi = testSuite.GetType()
               .GetMethods()
               .FirstOrDefault(m =>
               {
                   return m.IsDefined(typeof(AfterTestAttribute));
               });
        }

        public void Execute()
        {
            if (_mi != null)
            {
                Godot.GD.PrintS("AfterTestExecutionLayer execute");
                _mi.Invoke(_testSuite, new object[] { });
            }
        }
    }
}
