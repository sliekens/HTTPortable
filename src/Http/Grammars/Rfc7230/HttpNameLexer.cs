using System.Linq;
using Text.Scanning;

namespace Http.Grammars.Rfc7230
{
    public class HttpNameLexer : Lexer<HttpNameToken>
    {
        public override HttpNameToken Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            HttpNameToken token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(context, "Expected HTTP-name");
        }

        public override bool TryRead(ITextScanner scanner, out HttpNameToken token)
        {
            var context = scanner.GetContext();
            if ("HTTP".ToCharArray().All(scanner.TryMatch))
            {
                token = new HttpNameToken(context);
                return true;
            }

            token = default(HttpNameToken);
            return false;
        }
    }
}
