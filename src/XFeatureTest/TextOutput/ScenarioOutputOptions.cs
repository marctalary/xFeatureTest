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

        public int MaxPrefixLength => StatementOptions.Where(s => s.IndentWithOtherStatements).Max(s => s.PrefixLength);
    }
}