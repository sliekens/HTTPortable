using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class ObsoletedTextLexer : Lexer<ObsoletedText>
    {
        public ObsoletedTextLexer()
            : base("obs-text")
        {
        }

        public override bool TryRead(ITextScanner scanner, out ObsoletedText element)
        {
            if (scanner.EndOfInput)
            {
                element = default(ObsoletedText);
                return false;
            }

            var context = scanner.GetContext();
            for (var c = '\u0080'; c <= '\u00FF'; c++)
            {
                if (scanner.TryMatch(c))
                {
                    element = new ObsoletedText(c, context);
                    return true;
                }
            }

            element = default(ObsoletedText);
            return false;
        }
    }
}