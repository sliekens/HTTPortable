using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class ObsFoldLexer : Lexer<ObsFoldToken>
    {
        private readonly ILexer<CrLfToken> crLfLexer;
        private readonly ILexer<SpToken> spLexer;
        private readonly ILexer<HTabToken> hTabLexer;

        public ObsFoldLexer()
            : this(new CrLfLexer(), new SpLexer(), new HTabLexer())
        {
        }

        public ObsFoldLexer(ILexer<CrLfToken> crLfLexer, ILexer<SpToken> spLexer, ILexer<HTabToken> hTabLexer)
        {
            Contract.Requires(crLfLexer != null);
            Contract.Requires(spLexer != null);
            Contract.Requires(hTabLexer != null);
            this.crLfLexer = crLfLexer;
            this.spLexer = spLexer;
            this.hTabLexer = hTabLexer;
        }

        public override ObsFoldToken Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            ObsFoldToken token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(context, "Expected 'obs-fold'");
        }

        public override bool TryRead(ITextScanner scanner, out ObsFoldToken token)
        {
            if (scanner.EndOfInput)
            {
                token = default(ObsFoldToken);
                return false;
            }

            var context = scanner.GetContext();
            CrLfToken crLf;
            if (!this.crLfLexer.TryRead(scanner, out crLf))
            {
                token = default(ObsFoldToken);
                return false;
            }

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
                    this.crLfLexer.PutBack(scanner, crLf);
                    token = default(ObsFoldToken);
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

            token = new ObsFoldToken(crLf, tokens, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.crLfLexer != null);
            Contract.Invariant(this.spLexer != null);
            Contract.Invariant(this.hTabLexer != null);
        }

    }
}
