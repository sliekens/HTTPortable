using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class ObsTextLexer : Lexer<ObsTextToken>
    {
        public override ObsTextToken Read(ITextScanner scanner)
        {
            ObsTextToken token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(scanner.GetContext(), "Expected 'obs-text'");
        }

        public override bool TryRead(ITextScanner scanner, out ObsTextToken token)
        {
            if (scanner.EndOfInput)
            {
                token = default(ObsTextToken);
                return false;
            }

            var context = scanner.GetContext();
            for (var c = '\u0080'; c <= '\u00FF'; c++)
            {
                if (scanner.TryMatch(c))
                {
                    token = new ObsTextToken(c, context);
                    return true;
                }
            }

            token = default(ObsTextToken);
            return false;
        }
    }
}