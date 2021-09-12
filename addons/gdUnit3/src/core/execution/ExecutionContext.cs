using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GdUnit3
{
    public sealed class ExecutionContext : IDisposable
    {
        public ExecutionContext(TestSuite testInstance, IEnumerable<ITestEventListener> eventListeners)
        {
            Stopwatch = new Stopwatch();
            Stopwatch.Start();

            TestInstance = testInstance;
            EventListeners = eventListeners;
            ReportCollector = new TestReportCollector();
            SubExecutionContexts = new List<ExecutionContext>();
            // fake report consumer for now, will be replaced by TestEvent listener
            testInstance.SetMeta("gdunit.report.consumer", ReportCollector);
        }
        public ExecutionContext(ExecutionContext context) : this(context.TestInstance, context.EventListeners)
        {
            context.SubExecutionContexts.Add(this);
            // Test = context.Test ?? null;
            // Skipped = Test?.Skipped ?? false;
            // CurrentIteration = Test?.Attributes.iterations ?? 0;
        }

        public ExecutionContext(ExecutionContext context, TestCase testCase) : this(context.TestInstance, context.EventListeners)
        {
            context.SubExecutionContexts.Add(this);
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

#nullable enable
        public List<ExecutionContext> SubExecutionContexts
        { get; private set; }
#nullable disable

        public TestCase Test
        { get; set; }

        private bool Skipped
        { get; set; }

        public int Duration
        { get => (int)Stopwatch.ElapsedMilliseconds; }

        private int _iteration;
        public int CurrentIteration
        {
            get => _iteration--;
            set => _iteration = value;
        }

        public TestReportCollector ReportCollector
        { get; private set; }


        public bool IsFailed()
        {
            return ReportCollector.Failures.Count() > 0 || SubExecutionContexts.Where(context => context.IsFailed()).Count() != 0;
        }

        public bool IsError()
        {
            return ReportCollector.Errors.Count() > 0 || SubExecutionContexts.Where(context => context.IsError()).Count() != 0;
        }

        public bool IsWarning()
        {
            return ReportCollector.Warnings.Count() > 0 || SubExecutionContexts.Where(context => context.IsWarning()).Count() != 0;
        }

        public bool IsSkipped() => Skipped;

        public int SkippedCount() => SubExecutionContexts.Select(context => context.SkippedCount()).Sum() + (IsSkipped() ? 1 : 0);

        public IEnumerable BuildStatistics()
        {
            var failures = ReportCollector.Failures;
            var errors = ReportCollector.Errors;
            return TestEvent.BuildStatistics(0,
                IsError(), errors.Count(),
                IsFailed(), failures.Count(),
                false,
                IsSkipped(), SkippedCount(),
                Duration);
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

        public void PrintDebug(string name = "")
        {
            Godot.GD.PrintS(name, "test context", TestInstance.Name, Test?.Name, "error:" + IsError(), "failed:" + IsFailed(), "skipped:" + IsSkipped());
        }
    }

}
