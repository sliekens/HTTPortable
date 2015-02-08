using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class ReasonPhraseToken : Token
    {
        private readonly IList<Token> tokens;

        public ReasonPhraseToken(IList<Token> tokens, ITextContext context)
            : base(string.Concat(tokens), context)
        {
            Contract.Requires(tokens != null);
            Contract.Requires(Contract.ForAll(tokens,
                token => token is HTabToken || token is SpToken || token is VCharToken || token is ObsTextToken));
            this.tokens = tokens;
        }

        public IList<Token> Tokens
        {
            get
            {
                return this.tokens;
            }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.tokens != null);
        }
    }
}