using Godot;
using Godot.Collections;
using Array = Godot.Collections.Array;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GdUnit3
{
    public class CsTools : Reference
    {
        public Array GetTestCases(String className)
        {
            System.Type type = System.Type.GetType(className);
            Array methods = new Array();
            List<MethodInfo> methodInfos = new List<MethodInfo>(type.GetMethods().Where(m => m.IsDefined(typeof(GdUnitTestSuite.TestCaseAttribute))).ToList());
            foreach (var methodInfo in methodInfos)
            {
                var attributes = methodInfo.GetCustomAttribute<GdUnitTestSuite.TestCaseAttribute>();

                methods.Add(new Dictionary {
                    { "name", methodInfo.Name },
                    { "line_number", attributes.Line }
                });
            }
            return methods;
        }

        public bool IsTestSuite(String className)
        {
            System.Type type = System.Type.GetType(className);
            if (type == null)
            {
                return false;
            }
            return Attribute.GetCustomAttribute(type, typeof(GdUnitTestSuite.TestSuiteAttribute)) != null;
        }

    }
}