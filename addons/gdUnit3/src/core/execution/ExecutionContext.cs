using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace GdUnit3
{
    public sealed class ExecutionContext : IDisposable
    {
        public ExecutionContext(TestSuite testInstance, IEnumerable<ITestEventListener> eventListeners)
        {
            TestInstance = testInstance;
            EventListeners = eventListeners;
            Skipped = testInstance.Skipped;

            Stopwatch = new Stopwatch();
            Stopwatch.Start();
        }
        public ExecutionContext(ExecutionContext context) : this(context.TestInstance, context.EventListeners)
        {
            Test = context.Test ?? null;
            Skipped = Test?.Skipped ?? false;
            CurrentIteration = Test?.Attributes.iterations ?? 0;
        }

        public ExecutionContext(ExecutionContext context, TestCase testCase) : this(context.TestInstance, context.EventListeners)
        {
            Test = testCase;
            CurrentIteration = testCase.Attributes.iterations;
            Skipped = Test.Skipped;
        }

        public Stopwatch Stopwatch
        { get; private set; }

        public TestSuite TestInstance
        { get; private set; }

        private IEnumerable<ITestEventListener> EventListeners
        { get; set; }


        public TestCase Test
        { get; set; }

        public bool Skipped
        { get; set; }

        public int Duration
        { get => (int)Stopwatch.ElapsedMilliseconds; }

        private int _iteration;
        public int CurrentIteration
        {
            get => _iteration--;
            set => _iteration = value;
        }

        public void FireTestEvent(TestEvent e)
        {
            EventListeners.ToList()
                .ForEach(l => l.PublishEvent(e));
        }

        public void Dispose()
        {
            Stopwatch.Stop();
        }

        public void PrintDebug()
        {
            Godot.GD.PrintS("test context", TestInstance, Test, CurrentIteration, Skipped);
        }
    }

}
