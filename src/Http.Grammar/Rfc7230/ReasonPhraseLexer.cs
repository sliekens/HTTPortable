using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class ReasonPhraseLexer : Lexer<ReasonPhrase>
    {
        private readonly ILexer<HorizontalTab> hTabLexer;
        private readonly ILexer<ObsText> obsTextLexer;
        private readonly ILexer<Space> SpaceLexer;
        private readonly ILexer<VisibleCharacter> vCharLexer;

        public ReasonPhraseLexer()
            : this(new HorizontalTabLexer(), new SpaceLexer(), new VisibleCharacterLexer(), new ObsTextLexer())
        {
        }

        public ReasonPhraseLexer(ILexer<HorizontalTab> hTabLexer, ILexer<Space> SpaceLexer, ILexer<VisibleCharacter> vCharLexer,
            ILexer<ObsText> obsTextLexer)
        {
            Contract.Requires(hTabLexer != null);
            Contract.Requires(SpaceLexer != null);
            Contract.Requires(vCharLexer != null);
            Contract.Requires(obsTextLexer != null);
            this.hTabLexer = hTabLexer;
            this.SpaceLexer = SpaceLexer;
            this.vCharLexer = vCharLexer;
            this.obsTextLexer = obsTextLexer;
        }

        public override ReasonPhrase Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            ReasonPhrase token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            // This code should never be reached, because reason-phrase can be an empty string
            throw new SyntaxErrorException(context, "Expected 'reason-phrase'");
        }

        public override bool TryRead(ITextScanner scanner, out ReasonPhrase token)
        {
            var context = scanner.GetContext();
            var tokens = new List<Element>();
            while (!scanner.EndOfInput)
            {
                HorizontalTab hTabToken;
                Space Space;
                VisibleCharacter vCharToken;
                ObsText obsText;
                if (this.hTabLexer.TryRead(scanner, out hTabToken))
                {
                    tokens.Add(hTabToken);
                }
                else if (this.SpaceLexer.TryRead(scanner, out Space))
                {
                    tokens.Add(Space);
                }
                else if (this.vCharLexer.TryRead(scanner, out vCharToken))
                {
                    tokens.Add(vCharToken);
                }
                else if (this.obsTextLexer.TryRead(scanner, out obsText))
                {
                    tokens.Add(obsText);
                }
                else
                {
                    break;
                }
            }

            token = new ReasonPhrase(tokens, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.hTabLexer != null);
            Contract.Invariant(this.SpaceLexer != null);
            Contract.Invariant(this.vCharLexer != null);
            Contract.Invariant(this.obsTextLexer != null);
        }
    }
}