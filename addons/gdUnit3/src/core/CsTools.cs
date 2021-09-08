using Godot;
using System;
using System.Diagnostics.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GdUnit3
{
    public class CsTools : Reference
    {
        // used from GdScript side, will be remove later
        public static Array ListTestCases(String className)
        {
            System.Type type = System.Type.GetType(className);
            Contract.Requires(Attribute.IsDefined(type, typeof(TestSuiteAttribute)));
            return type.GetMethods()
                .Where(m => m.IsDefined(typeof(TestCaseAttribute)))
                .Select(mi => new TestCase(null, mi))
                .ToArray();
        }

        // used from GdScript side, will be remove later
        public static bool IsTestSuite(String className)
        {
            System.Type type = System.Type.GetType(className);
            if (type == null)
            {
                return false;
            }
            return Attribute.IsDefined(type, typeof(TestSuiteAttribute));
        }

        public static IEnumerable<TestCase> GetTestCases(TestSuite testSuite)
        {
            Contract.Requires(Attribute.IsDefined(testSuite.GetType(), typeof(TestSuiteAttribute)));
            return testSuite.GetType().GetMethods()
                .Where(m => m.IsDefined(typeof(TestCaseAttribute)))
                .Select(mi => new TestCase(testSuite, mi));
        }

        public static System.Collections.Generic.IEnumerable<object> GetTestMethodParameters(MethodInfo methodInfo)
        {
            return methodInfo.GetParameters()
                .SelectMany(pi => pi.GetCustomAttributesData()
                    .Where(attr => attr.AttributeType == typeof(FuzzerAttribute))
                    .Select(attr =>
                    {
                        var arguments = attr.ConstructorArguments.Select(arg => arg.Value).ToArray();
                        return attr.Constructor.Invoke(arguments);
                    }
                )
             );
        }
    }
}
