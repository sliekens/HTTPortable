using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class MethodToken : Token
    {
        private readonly TokenToken token;

        public MethodToken(TokenToken token, ITextContext context)
            : base(token.Data, context)
        {
            Contract.Requires(token != null);
            this.token = token;
        }

        public TokenToken Token
        {
            get { return token; }
        }
    }
}
