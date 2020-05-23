using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using XFeatureTest.Hosting;
using XFeatureTest.TextFormatters;
using Xunit.Abstractions;

namespace XFeatureTest.TextOutput
{
    public class ScenarioOutput
    {
        private readonly List<string> _displayedPrefixes = new List<string>();
        private readonly ScenarioOutputOptions _outputOptions;
        private readonly TestContext _testContext;

        public ScenarioOutput(TestContext testContext, ScenarioOutputOptions outputOptions)
        {
            _testContext = testContext;
            _outputOptions = outputOptions;
        }

        private ITestOutputHelper Output => _testContext.Output;


        public void Cleanup([CallerMemberName] string cleanupDescription = null)
        {
            Output.WriteLine(BuildScenarioText(cleanupDescription, "CLEANUP", _outputOptions));
        }

        public void Feature([CallerMemberName] string scenario = null)
        {
            Output.WriteLine(BuildScenarioText(scenario, "FEATURE", _outputOptions));
        }

        public void Given([CallerMemberName] string preconditionDescription = null)
        {
            Output.WriteLine(BuildScenarioText(preconditionDescription, "GIVEN", _outputOptions));
        }

        public void Scenario([CallerMemberName] string scenarioDescription = null)
        {
            Output.WriteLine(BuildScenarioText(scenarioDescription, "SCENARIO", _outputOptions));
        }

        public void Then([CallerMemberName] string assertionDescription = null)
        {
            Output.WriteLine(BuildScenarioText(assertionDescription, "THEN", _outputOptions));
        }

        public void When([CallerMemberName] string actionDescription = null)
        {
            Output.WriteLine(BuildScenarioText(actionDescription, "WHEN", _outputOptions));
        }

        private string BuildScenarioText(string text, string statementType, ScenarioOutputOptions outputOptions)
        {
            var statementOptions = outputOptions.StatementOptions.Single(s => s.Prefix == statementType);

            var firstOccurence = !_displayedPrefixes.Contains(statementOptions.Prefix);
            var sb = new StringBuilder();

            string prefix;
            if (firstOccurence)
            {
                _displayedPrefixes.Add(statementOptions.Prefix);

                if (statementOptions.InsertLineSpaceBeforeFirstOccurrence)
                    sb.AppendLine(statementOptions.LineSpaceText);

                prefix = statementOptions.Prefix;
            }
            else
            {
                prefix = statementOptions.PrefixAfterFirstOccurence;
            }

            if (outputOptions.PrefixStatementsWithDate)
                sb.Append(DateTime.Now.ToString(outputOptions.PrefixStatementsWithDateTimeFormat)).Append(" ");

            if (statementOptions.IndentWithOtherStatements)
                prefix = firstOccurence
                    ? prefix.PadRight(outputOptions.MaxPrefixLength)
                    : prefix.PadLeft(outputOptions.MaxPrefixLength);

            sb.Append(prefix)
                .Append(" ")
                .Append(ApplyTextFormatters(text, outputOptions.TextFormatters));

            return sb.ToString();
        }

        public string ApplyTextFormatters(string text, IEnumerable<TextFormatter> textFormatters)
        {
            foreach (var textFormatter in textFormatters) text = textFormatter.Format(text);
            return text;
        }
    }
}