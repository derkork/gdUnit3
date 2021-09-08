using Godot.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System;

namespace GdUnit3
{
    public sealed class TestCase : Godot.Node, IDisposable
    {
        public TestCase(TestSuite testSuite, MethodInfo methodInfo)
        {
            this.TestSuite = testSuite;
            this.Name = methodInfo.Name;
            this.Test = methodInfo;
            this.Parameters = CsTools.GetTestMethodParameters(methodInfo).ToArray();
        }

        ~TestCase()
        {
            this.TestSuite = null;
        }

        public TestCaseAttribute Attributes
        { get => Test.GetCustomAttribute<TestCaseAttribute>(); }


        public bool IsSkipped => Attribute.IsDefined(Test, typeof(IgnoreUntilAttribute));

        public Godot.Collections.Dictionary attributes()
        {
            var attributes = Attributes;
            return new Dictionary {
                    { "name", Name },
                    { "line_number", attributes.line },
                    { "timeout", attributes.timeout },
                    { "iterations", attributes.iterations },
                    { "seed", attributes.seed },
                };
        }

        public int line_number()
        {
            return Attributes.line;
        }

        public bool is_skipped()
        {
            return false;
        }

        public void generate_seed()
        {

        }

        public bool has_fuzzer()
        {
            return false;
        }

        public void set_parent(Godot.Node testSuite)
        {
            TestSuite = testSuite as TestSuite;
        }

        public bool is_interupted()
        {
            return false;
        }

        public bool is_expect_interupted()
        {
            return false;
        }

        public int timeout()
        {
            return Attributes.timeout;
        }

        private IEnumerable<object> Parameters
        { get; set; }

        private MethodInfo Test
        { get; set; }

        public TestSuite TestSuite
        { get; set; }

        private IEnumerable<object> ResolveParam(object input)
        {
            if (input is IValueProvider)
            {
                return (input as IValueProvider).GetValues();
            }
            return new object[] { input };
        }

        public void execute()
        {
            Godot.GD.PrintS("TestCase ->", Name);
            object[] arguments = Parameters.SelectMany(ResolveParam).ToArray<object>();
            Test.Invoke(TestSuite, arguments);
        }
    }
}
