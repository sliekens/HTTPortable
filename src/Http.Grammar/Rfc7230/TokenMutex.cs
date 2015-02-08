using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class TokenMutex<T1, T2>
        where T1 : Token
        where T2 : Token
    {
        private readonly T1 token1;
        private readonly T2 token2;


        public TokenMutex(T1 token)
        {
            Contract.Requires(token != null);
            this.token1 = token;
        }

        public TokenMutex(T2 token)
        {
            Contract.Requires(token != null);
            this.token2 = token;
        }

        public Token Token
        {
            get
            {
                return this.token1 as Token ?? this.token2;
            }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.token1 == null || this.token2 == null);
        }
    }
}
