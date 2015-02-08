using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class RWSLexer : Lexer<RWSToken>
    {
        private readonly ILexer<SpToken> spLexer;
        private readonly ILexer<HTabToken> hTabLexer;

        public RWSLexer()
            : this(new SpLexer(), new HTabLexer())
        {
        }

        public RWSLexer(ILexer<SpToken> spLexer, ILexer<HTabToken> hTabLexer)
        {
            Contract.Requires(spLexer != null);
            Contract.Requires(hTabLexer != null);
            this.spLexer = spLexer;
            this.hTabLexer = hTabLexer;
        }

        public override RWSToken Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            RWSToken token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(context, "Expected 'RWS'");
        }

        public override bool TryRead(ITextScanner scanner, out RWSToken token)
        {
            if (scanner.EndOfInput)
            {
                token = default(RWSToken);
                return false;
            }

            var context = scanner.GetContext();
            SpToken sp;
            HTabToken hTab;
            IList<WspMutex> tokens = new List<WspMutex>();
            if (this.spLexer.TryRead(scanner, out sp))
            {
                tokens.Add(new WspMutex(sp));
            }
            else
            {
                if (hTabLexer.TryRead(scanner, out hTab))
                {
                    tokens.Add(new WspMutex(hTab));
                }
                else
                {
                    token = default(RWSToken);
                    return false;
                }
            }

            for (; ; )
            {
                if (spLexer.TryRead(scanner, out sp))
                {
                    tokens.Add(new WspMutex(sp));
                }
                else
                {
                    if (hTabLexer.TryRead(scanner, out hTab))
                    {
                        tokens.Add(new WspMutex(hTab));
                    }
                    else
                    {
                        break;
                    }
                }
            }

            token = new RWSToken(tokens, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.spLexer != null);
            Contract.Invariant(this.hTabLexer != null);
        }
    }
}
