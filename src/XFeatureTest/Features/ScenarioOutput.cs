using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using XFeatureTest.Hosting;
using Xunit.Abstractions;

namespace XFeatureTest.Features
{
    public class ScenarioOutput
    {
        private readonly List<string> _displayedPrefixes = new List<string>();
        private readonly TestContext _testContext;

        public ScenarioOutput(TestContext testContext)
        {
            _testContext = testContext;
        }

        private ITestOutputHelper Output => _testContext.Output;

        public void Cleanup([CallerMemberName] string cleanupDescription = null, bool isCamelOrPascalCase = true)
        {
            Output.WriteLine(BuildScenarioText(cleanupDescription, "CLEANUP", isCamelOrPascalCase, true));
        }

        public void Feature([CallerMemberName] string scenario = null, bool isCamelOrPascalCase = true)
        {
            Output.WriteLine(BuildScenarioText(scenario, "FEATURE", isCamelOrPascalCase));
        }

        public void Given([CallerMemberName] string preconditionDescription = null, bool isCamelOrPascalCase = true)
        {
            Output.WriteLine(BuildScenarioText(preconditionDescription, "GIVEN", isCamelOrPascalCase, true));
        }

        public void Scenario([CallerMemberName] string scenarioDescription = null, bool isCamelOrPascalCase = true)
        {
            Output.WriteLine(BuildScenarioText(scenarioDescription, "SCENARIO", isCamelOrPascalCase));
        }

        public void Then([CallerMemberName] string assertionDescription = null, bool isCamelOrPascalCase = true)
        {
            Output.WriteLine(BuildScenarioText(assertionDescription, "THEN ", isCamelOrPascalCase));
        }

        public void When([CallerMemberName] string actionDescription = null, bool isCamelOrPascalCase = true)
        {
            Output.WriteLine(BuildScenarioText(actionDescription, "WHEN ", isCamelOrPascalCase));
        }

        private string BuildScenarioText(string text, string prefix, bool isCamelOrPascalCase,
            bool insertLineSpaceOnFirstOccurrence = false)
        {
            var sb = new StringBuilder();
            var time = DateTime.Now.ToString("HH:mm:ss ");

            if (_displayedPrefixes.Contains(prefix))
            {
                sb.Append(time).Append("  AND ");
            }
            else
            {
                if (insertLineSpaceOnFirstOccurrence)
                    sb.AppendLine("----------------");

                sb.Append(time).Append(prefix).Append(" ");

                _displayedPrefixes.Add(prefix);
            }

            sb.Append(isCamelOrPascalCase ? CamelCaseToText(text) : text);

            return sb.ToString();
        }

        private string CamelCaseToText(string camelOrPascalCaseString)
        {
            var sb = new StringBuilder();

            var startOfWord = true;
            foreach (var ch in camelOrPascalCaseString)
            {
                if (char.IsUpper(ch))
                    if (!startOfWord)
                        sb.Append(" ");

                sb.Append(ch.ToString().ToLower());

                startOfWord = char.IsWhiteSpace(ch);
            }

            return sb.ToString();
        }
    }
}