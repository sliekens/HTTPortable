using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class ObsTextLexer : Lexer<ObsText>
    {
        public override ObsText Read(ITextScanner scanner)
        {
            ObsText token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(scanner.GetContext(), "Expected 'obs-text'");
        }

        public override bool TryRead(ITextScanner scanner, out ObsText token)
        {
            if (scanner.EndOfInput)
            {
                token = default(ObsText);
                return false;
            }

            var context = scanner.GetContext();
            for (var c = '\u0080'; c <= '\u00FF'; c++)
            {
                if (scanner.TryMatch(c))
                {
                    token = new ObsText(c, context);
                    return true;
                }
            }

            token = default(ObsText);
            return false;
        }
    }
}