namespace XFeatureTest.TextOutput
{
    public class ScenarioStatementOptions
    {
        public string Prefix { get; set; }
        public string PrefixAfterFirstOccurence { get; set; }
        public bool InsertLineSpaceBeforeFirstOccurrence { get; set; }
        public string LineSpaceText { get; set; }
        public bool IndentWithOtherStatements { get; set; }

        public int PrefixLength => Prefix.Length > PrefixAfterFirstOccurence.Length
            ? Prefix.Length
            : PrefixAfterFirstOccurence.Length;
    }
}