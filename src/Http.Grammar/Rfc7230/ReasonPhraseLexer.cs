using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class ReasonPhraseLexer : Lexer<ReasonPhrase>
    {
        private readonly ILexer<HorizontalTab> hTabLexer;
        private readonly ILexer<ObsoletedText> obsTextLexer;
        private readonly ILexer<Space> SpaceLexer;
        private readonly ILexer<VisibleCharacter> vCharLexer;

        public ReasonPhraseLexer()
            : this(new HorizontalTabLexer(), new SpaceLexer(), new VisibleCharacterLexer(), new ObsoletedTextLexer())
        {
        }

        public ReasonPhraseLexer(ILexer<HorizontalTab> hTabLexer, ILexer<Space> SpaceLexer, ILexer<VisibleCharacter> vCharLexer,
            ILexer<ObsoletedText> obsTextLexer)
            : base("reason-phrase")
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

        public override bool TryRead(ITextScanner scanner, out ReasonPhrase element)
        {
            var context = scanner.GetContext();
            var elements = new List<Alternative<HorizontalTab, Space, VisibleCharacter, ObsoletedText>>();
            while (!scanner.EndOfInput)
            {
                var currentContext = scanner.GetContext();
                HorizontalTab horizontalTab;
                Space space;
                VisibleCharacter visibleCharacter;
                ObsoletedText obsoletedText;
                if (this.hTabLexer.TryRead(scanner, out horizontalTab))
                {
                    elements.Add(new Alternative<HorizontalTab, Space, VisibleCharacter, ObsoletedText>(horizontalTab, currentContext));
                }
                else if (this.SpaceLexer.TryRead(scanner, out space))
                {
                    elements.Add(new Alternative<HorizontalTab, Space, VisibleCharacter, ObsoletedText>(space, currentContext));
                }
                else if (this.vCharLexer.TryRead(scanner, out visibleCharacter))
                {
                    elements.Add(new Alternative<HorizontalTab, Space, VisibleCharacter, ObsoletedText>(visibleCharacter, currentContext));
                }
                else if (this.obsTextLexer.TryRead(scanner, out obsoletedText))
                {
                    elements.Add(new Alternative<HorizontalTab, Space, VisibleCharacter, ObsoletedText>(obsoletedText, currentContext));
                }
                else
                {
                    break;
                }
            }

            element = new ReasonPhrase(elements, context);
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