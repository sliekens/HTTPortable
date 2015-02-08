using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class ReasonPhraseLexer : Lexer<ReasonPhraseToken>
    {
        private readonly ILexer<HTabToken> hTabLexer;
        private readonly ILexer<ObsTextToken> obsTextLexer;
        private readonly ILexer<SpToken> spLexer;
        private readonly ILexer<VCharToken> vCharLexer;

        public ReasonPhraseLexer()
            : this(new HTabLexer(), new SpLexer(), new VCharLexer(), new ObsTextLexer())
        {
        }

        public ReasonPhraseLexer(ILexer<HTabToken> hTabLexer, ILexer<SpToken> spLexer, ILexer<VCharToken> vCharLexer,
            ILexer<ObsTextToken> obsTextLexer)
        {
            Contract.Requires(hTabLexer != null);
            Contract.Requires(spLexer != null);
            Contract.Requires(vCharLexer != null);
            Contract.Requires(obsTextLexer != null);
            this.hTabLexer = hTabLexer;
            this.spLexer = spLexer;
            this.vCharLexer = vCharLexer;
            this.obsTextLexer = obsTextLexer;
        }

        public override ReasonPhraseToken Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            ReasonPhraseToken token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            // This code should never be reached, because reason-phrase can be an empty string
            throw new SyntaxErrorException(context, "Expected 'reason-phrase'");
        }

        public override bool TryRead(ITextScanner scanner, out ReasonPhraseToken token)
        {
            var context = scanner.GetContext();
            var tokens = new List<Token>();
            while (!scanner.EndOfInput)
            {
                HTabToken hTabToken;
                SpToken spToken;
                VCharToken vCharToken;
                ObsTextToken obsTextToken;
                if (this.hTabLexer.TryRead(scanner, out hTabToken))
                {
                    tokens.Add(hTabToken);
                }
                else if (this.spLexer.TryRead(scanner, out spToken))
                {
                    tokens.Add(spToken);
                }
                else if (this.vCharLexer.TryRead(scanner, out vCharToken))
                {
                    tokens.Add(vCharToken);
                }
                else if (this.obsTextLexer.TryRead(scanner, out obsTextToken))
                {
                    tokens.Add(obsTextToken);
                }
                else
                {
                    break;
                }
            }

            token = new ReasonPhraseToken(tokens, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.hTabLexer != null);
            Contract.Invariant(this.spLexer != null);
            Contract.Invariant(this.vCharLexer != null);
            Contract.Invariant(this.obsTextLexer != null);
        }
    }
}