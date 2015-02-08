using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class OWSLexer : Lexer<OWSToken>
    {
        private readonly HTabLexer hTabLexer;
        private readonly SpLexer spLexer;

        public OWSLexer()
            : this(new SpLexer(), new HTabLexer())
        {
        }

        public OWSLexer(SpLexer spLexer, HTabLexer hTabLexer)
        {
            Contract.Requires(spLexer != null);
            Contract.Requires(hTabLexer != null);
            this.spLexer = spLexer;
            this.hTabLexer = hTabLexer;
        }

        public override OWSToken Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            OWSToken token;
            if (TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(context, "Expected 'OWS'");
        }

        public override bool TryRead(ITextScanner scanner, out OWSToken token)
        {
            if (scanner.EndOfInput)
            {
                token = default(OWSToken);
                return false;
            }

            var context = scanner.GetContext();
            IList<OWSToken.OWSMutex> tokens = new List<OWSToken.OWSMutex>();
            for (;;)
            {
                SpToken sp;
                if (spLexer.TryRead(scanner, out sp))
                {
                    tokens.Add(new OWSToken.OWSMutex(sp));
                }
                else
                {
                    HTabToken hTab;
                    if (hTabLexer.TryRead(scanner, out hTab))
                    {
                        tokens.Add(new OWSToken.OWSMutex(hTab));
                    }
                    else
                    {
                        break;
                    }
                }
            }

            token = new OWSToken(tokens, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(spLexer != null);
            Contract.Invariant(hTabLexer != null);
        }
    }
}