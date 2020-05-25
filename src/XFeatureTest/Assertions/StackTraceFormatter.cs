using System;
using System.Collections.Generic;

namespace XFeatureTest.Assertions
{
    public static class StackTraceFormatter
    {
        public static string RemoveAssertionTraces(string stackTrace)
        {
            // Remove any entries in the stack trace from the current names
            var traces = new List<string>();
            traces.AddRange(stackTrace.Split(new[]
            {
                Environment.NewLine
            }, StringSplitOptions.None));

            // ReSharper disable once AssignNullToNotNullAttribute
            traces.RemoveAll(x => x.Contains(typeof(StackTraceFormatter).Namespace));

            return string.Join(Environment.NewLine, traces.ToArray());
        }
    }
}
