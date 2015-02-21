using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class ObsoletedTextLexer : Lexer<ObsoletedText>
    {
        public override ObsoletedText Read(ITextScanner scanner)
        {
            ObsoletedText token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(scanner.GetContext(), "Expected 'obs-text'");
        }

        public override bool TryRead(ITextScanner scanner, out ObsoletedText token)
        {
            if (scanner.EndOfInput)
            {
                token = default(ObsoletedText);
                return false;
            }

            var context = scanner.GetContext();
            for (var c = '\u0080'; c <= '\u00FF'; c++)
            {
                if (scanner.TryMatch(c))
                {
                    token = new ObsoletedText(c, context);
                    return true;
                }
            }

            token = default(ObsoletedText);
            return false;
        }
    }
}