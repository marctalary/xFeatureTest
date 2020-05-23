using System.Collections.Generic;
using XFeatureTest.TextFormatters;

namespace XFeatureTest.TextOutput
{
    public class ScenarioOutputOptionsFactory
    {
        public static ScenarioOutputOptions DefaultOptions
        {
            get
            {
                var defaultOptions = new ScenarioOutputOptions
                {
                    PrefixStatementsWithDate = true,
                    PrefixStatementsWithDateTimeFormat = "HH:mm:ss",
                    TextFormatters =
                        new List<TextFormatter> {new CamelCaseFormatter(), new UnderscoreSpacedFormatter()},
                    StatementOptions = new List<ScenarioStatementOptions>
                    {
                        new ScenarioStatementOptions
                        {
                            Prefix = "SCENARIO"
                        },
                        new ScenarioStatementOptions
                        {
                            Prefix = "FEATURE"
                        },
                        new ScenarioStatementOptions
                        {
                            Prefix = "GIVEN",
                            InsertLineSpaceBeforeFirstOccurrence = true,
                            LineSpaceText = "----------------",
                            PrefixAfterFirstOccurence = "AND",
                            IndentWithOtherStatements = true
                        },
                        new ScenarioStatementOptions
                        {
                            Prefix = "WHEN",
                            PrefixAfterFirstOccurence = "AND",
                            IndentWithOtherStatements = true
                        },
                        new ScenarioStatementOptions
                        {
                            Prefix = "THEN",
                            PrefixAfterFirstOccurence = "AND",
                            IndentWithOtherStatements = true
                        },
                        new ScenarioStatementOptions
                        {
                            Prefix = "CLEANUP",
                            InsertLineSpaceBeforeFirstOccurrence = true,
                            LineSpaceText = "----------------"
                        }
                    }
                };

                return defaultOptions;
            }
        }
    }
}