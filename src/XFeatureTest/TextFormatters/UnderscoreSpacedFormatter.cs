namespace XFeatureTest.TextFormatters
{
    public class UnderscoreSpacedFormatter : TextFormatter
    {
        public override string Format(string text)
        {
            return text.Replace('_', ' ');
        }
    }
}