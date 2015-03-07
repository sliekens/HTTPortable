namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;

    using Text.Scanning;
    using Text.Scanning.Core;

    public class QuotedPairLexer : Lexer<QuotedPair>
    {
        private readonly ILexer<HorizontalTab> horizontalTabLexer;

        private readonly ILexer<Space> spaceLexer;

        private readonly ILexer<VisibleCharacter> visibleCharacterLexer;

        private readonly ILexer<ObsoletedText> obsoletedTextLexer;

        public QuotedPairLexer()
            : this(new HorizontalTabLexer(), new SpaceLexer(), new VisibleCharacterLexer(), new ObsoletedTextLexer())
        {
        }

        public QuotedPairLexer(ILexer<HorizontalTab> horizontalTabLexer, ILexer<Space> spaceLexer, ILexer<VisibleCharacter> visibleCharacterLexer, ILexer<ObsoletedText> obsoletedTextLexer)
            : base("quoted-pair")
        {
            Contract.Requires(horizontalTabLexer != null);
            Contract.Requires(spaceLexer != null);
            Contract.Requires(visibleCharacterLexer != null);
            Contract.Requires(obsoletedTextLexer != null);
            this.horizontalTabLexer = horizontalTabLexer;
            this.spaceLexer = spaceLexer;
            this.visibleCharacterLexer = visibleCharacterLexer;
            this.obsoletedTextLexer = obsoletedTextLexer;
        }

        public override bool TryRead(ITextScanner scanner, out QuotedPair element)
        {
            if (scanner.EndOfInput)
            {
                element = default(QuotedPair);
                return false;
            }

            var context = scanner.GetContext();
            Element backslash;
            if (!TryReadTerminal(scanner, '\\', out backslash))
            {
                element = default(QuotedPair);
                return false;
            }

            HorizontalTab horizontalTab;
            if (this.horizontalTabLexer.TryRead(scanner, out horizontalTab))
            {
                element = new QuotedPair(backslash, horizontalTab, context);
                return true;
            }

            Space space;
            if (this.spaceLexer.TryRead(scanner, out space))
            {
                element = new QuotedPair(backslash, space, context);
                return true;
            }

            VisibleCharacter visibleCharacter;
            if (this.visibleCharacterLexer.TryRead(scanner, out visibleCharacter))
            {
                element = new QuotedPair(backslash, visibleCharacter, context);
                return true;
            }

            ObsoletedText obsoletedText;
            if (this.obsoletedTextLexer.TryRead(scanner, out obsoletedText))
            {
                element = new QuotedPair(backslash, obsoletedText, context);
                return true;
            }

            scanner.PutBack(backslash.Data);
            element = default(QuotedPair);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.horizontalTabLexer != null);
            Contract.Invariant(this.spaceLexer != null);
            Contract.Invariant(this.visibleCharacterLexer != null);
            Contract.Invariant(this.obsoletedTextLexer != null);
        }
    }
}