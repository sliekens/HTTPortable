using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class FieldNameToken : Token
    {
        private readonly TokenToken token;

        public FieldNameToken(TokenToken token, ITextContext context)
            : base(token.Data, context)
        {
            Contract.Requires(token != null);
            this.token = token;
        }

        public TokenToken Token
        {
            get
            {
                return this.token;
            }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.token != null);
        }
    }
}
