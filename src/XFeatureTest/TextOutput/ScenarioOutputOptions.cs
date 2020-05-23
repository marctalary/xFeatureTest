using System.Collections.Generic;
using System.Linq;
using XFeatureTest.TextFormatters;

namespace XFeatureTest.TextOutput
{
    public class ScenarioOutputOptions
    {
        public List<TextFormatter> TextFormatters { get; set; } = new List<TextFormatter>();

        public List<ScenarioStatementOptions> StatementOptions { get; set; } = new List<ScenarioStatementOptions>();

        public bool PrefixStatementsWithDate { get; set; }

        public string PrefixStatementsWithDateTimeFormat { get; set; }

        public static ScenarioOutputOptions DefaultOptions { get; set; } = new ScenarioOutputOptions
        {
            PrefixStatementsWithDate = true,
            PrefixStatementsWithDateTimeFormat = "HH:mm:ss",
            TextFormatters = new List<TextFormatter> { new CamelCaseFormatter(), new UnderscoreSpacedFormatter() },
            StatementOptions = new List<ScenarioStatementOptions>
            {
                new ScenarioStatementOptions {Prefix = "SCENARIO", IndentWithOtherStatements = false},
                new ScenarioStatementOptions {Prefix = "FEATURE", IndentWithOtherStatements = false},
                new ScenarioStatementOptions {Prefix = "GIVEN", InsertLineSpaceBeforeFirstOccurrence = true},
                new ScenarioStatementOptions {Prefix = "WHEN"},
                new ScenarioStatementOptions {Prefix = "THEN"},
                new ScenarioStatementOptions {Prefix = "CLEANUP", InsertLineSpaceBeforeFirstOccurrence = true, IndentWithOtherStatements = false}
            }
        };

        public int MaxPrefixLength => StatementOptions.Where(s => s.IndentWithOtherStatements).Max(s => s.PrefixLength);
    }
}