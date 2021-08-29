using Godot;
using System;

namespace GdUnit3
{

    /** <summary>
    This class is the main class to implement your unit tests<br />
    You have to extend and implement your test cases as described<br />
    e.g<br />
    <br />
      For detailed instructions see <a href="https://github.com/MikeSchulze/gdUnit3/wiki/TestSuite">HERE</a> <br />
      
    <example>For example:
    
    <code>
    public class MyExampleTest : GdUnit3.GdUnitTestSuite
    {
         public void test_testCaseA()
         {
             assertThat("value").isEqual("value");
         }
     }
    </code>
    </example>
    </summary> */
    public abstract class GdUnitTestSuite : Node
    {


        [AttributeUsage(AttributeTargets.Class)]
        public class TestSuiteAttribute : Attribute
        {
        }

        [AttributeUsage(AttributeTargets.Method)]
        public class TestCaseAttribute : Attribute
        {
            public readonly int Timeout = -1;
            public readonly int Line;

            public TestCaseAttribute([System.Runtime.CompilerServices.CallerLineNumber] int line = 0)
            {
                Line = line;
            }
        }


        private bool _scipped = false;
        private String _active_test_case;

        private static Godot.Resource GdUnitTools = (Resource)GD.Load<GDScript>("res://addons/gdUnit3/src/core/GdUnitTools.gd").New();



        ~GdUnitTestSuite()
        {
        }

        /// <summary>
        /// This function is called before a test suite starts
        /// You can overwrite to prepare test data or initalizize necessary variables
        /// </summary>
        public virtual void Before() { }

        // This function is called at least when a test suite is finished
        // You can overwrite to cleanup data created during test running
        public virtual void After() { }

        // This function is called before a test case starts
        // You can overwrite to prepare test case specific data
        public virtual void BeforeTest() { }

        // This function is called after the test case is finished
        // You can overwrite to cleanup your test case specific data
        public virtual void AfterTest() { }

        // Skip the test-suite from execution, it will be ignored
        public void skip(bool skipped) => _scipped = skipped;

        public bool is_skipped => _scipped;

        public void set_active_test_case(String test_case) => _active_test_case = test_case;

        // === Tools ====================================================================
        // Mapps Godot error number to a readable error message. See at ERROR
        // https://docs.godotengine.org/de/stable/classes/class_@globalscope.html#enum-globalscope-error
        public String error_as_string(int error_number)
        {
            return (String)GdUnitTools.Call("error_as_string", error_number);
        }

        // A litle helper to auto freeing your created objects after test execution
        public T auto_free<T>(T obj)
        {
            GdUnitTools.Call("register_auto_free", obj, GetMeta("MEMORY_POOL"));
            return obj;
        }

        // Discard the error message triggered by a timeout (interruption).
        // By default, an interrupted test is reported as an error.
        // This function allows you to change the message to Success when an interrupted error is reported.
        public void discard_error_interupted_by_timeout()
        {
            //GdUnitTools.register_expect_interupted_by_timeout(self, __active_test_case)
        }

        // Creates a new directory under the temporary directory *user://tmp*
        // Useful for storing data during test execution. 
        // The directory is automatically deleted after test suite execution
        public String create_temp_dir(String relative_path)
        {
            //return GdUnitTools.create_temp_dir(relative_path)
            return "";
        }

        // Deletes the temporary base directory
        // Is called automatically after each execution of the test suite
        public void clean_temp_dir()
        {
            //GdUnitTools.clear_tmp()
        }

        // === Asserts ==================================================================
        public IGdUnitBoolAssert AssertBool(bool current, IGdUnitAssert.EXPECT expectResult = IGdUnitAssert.EXPECT.SUCCESS)
        {
            return new GdUnitBoolAssertWrapper(this, current, expectResult);
        }

        public IGdUnitStringAssert AssertString(string current, IGdUnitAssert.EXPECT expectResult = IGdUnitAssert.EXPECT.SUCCESS)
        {
            return new GdUnitStringAssertWrapper(this, current, expectResult);
        }

        public IGdUnitIntAssert AssertInt(int current, IGdUnitAssert.EXPECT expectResult = IGdUnitAssert.EXPECT.SUCCESS)
        {
            return new GdUnitIntAssertWrapper(this, current, expectResult);
        }

        public IGdUnitFloatAssert AssertFloat(double current, IGdUnitAssert.EXPECT expectResult = IGdUnitAssert.EXPECT.SUCCESS)
        {
            return new GdUnitFloatAssertWrapper(this, current, expectResult);
        }

        public IGdUnitObjectAssert AssertObject(object current, IGdUnitAssert.EXPECT expectResult = IGdUnitAssert.EXPECT.SUCCESS)
        {
            return new GdUnitObjectAssertWrapper(this, current, expectResult);
        }
    }

}
