namespace XFeatureTest.TextFormatters
{
    public   class UnderscoreSpacedFormatter: TextFormatter
    {
        public override string Format(string text)
            => text.Replace('_', ' ');
    }
}
