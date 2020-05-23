using System.Text;

namespace XFeatureTest.TextFormatters
{
    public class CamelCaseFormatter : TextFormatter {
        public override string Format(string text)
        {
            var sb = new StringBuilder();

            var startOfWord = true;
            foreach (var character in text)
            {
                if (char.IsUpper(character))
                    if (!startOfWord)
                        sb.Append(" ");

                sb.Append(character.ToString().ToLower());

                startOfWord = char.IsWhiteSpace(character);
            }

            return sb.ToString();
        }
    }
}
