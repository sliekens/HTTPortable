using System.Linq;
using Text.Scanning;

namespace Http.Grammars.Rfc7230
{
    public class HttpNameLexer : Lexer<HttpNameToken>
    {
        public HttpNameLexer(ITextScanner scanner)
            : base(scanner)
        {
        }

        public override HttpNameToken Read()
        {
            var context = this.Scanner.GetContext();
            HttpNameToken token;
            if (this.TryRead(out token))
            {
                return token;
            }

            throw new SyntaxErrorException("Expected HTTP-name", context);
        }

        public override bool TryRead(out HttpNameToken token)
        {
            var context = this.Scanner.GetContext();
            if ("HTTP".ToCharArray().All(this.Scanner.TryMatch))
            {
                token = new HttpNameToken(context);
                return true;
            }

            token = default(HttpNameToken);
            return false;
        }
    }
}
