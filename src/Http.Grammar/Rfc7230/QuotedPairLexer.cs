namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;
    using SLANG;
    using SLANG.Core;
    using EscapedCharacter = SLANG.Alternative<SLANG.Core.HorizontalTab, SLANG.Core.Space, SLANG.Core.VisibleCharacter, ObsoletedText>;

    public class QuotedPairLexer : Lexer<QuotedPair>
    {
        private readonly ILexer<HorizontalTab> horizontalTabLexer;
        private readonly ILexer<ObsoletedText> obsoletedTextLexer;
        private readonly ILexer<Space> spaceLexer;
        private readonly ILexer<VisibleCharacter> visibleCharacterLexer;

        public QuotedPairLexer()
            : this(new HorizontalTabLexer(), new SpaceLexer(), new VisibleCharacterLexer(), new ObsoletedTextLexer())
        {
        }

        public QuotedPairLexer(ILexer<HorizontalTab> horizontalTabLexer, ILexer<Space> spaceLexer,
            ILexer<VisibleCharacter> visibleCharacterLexer, ILexer<ObsoletedText> obsoletedTextLexer)
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

            EscapedCharacter escapedCharacter;
            if (this.TryReadEscapedCharacter(scanner, out escapedCharacter))
            {
                element = new QuotedPair(backslash, escapedCharacter, context);
                return true;
            }

            scanner.PutBack(backslash.Data);
            element = default(QuotedPair);
            return false;
        }

        private bool TryReadEscapedCharacter(ITextScanner scanner, out EscapedCharacter element)
        {
            if (scanner.EndOfInput)
            {
                element = default(EscapedCharacter);
                return false;
            }

            var context = scanner.GetContext();
            HorizontalTab horizontalTab;
            if (this.horizontalTabLexer.TryRead(scanner, out horizontalTab))
            {
                element = new EscapedCharacter(horizontalTab, context);
                return true;
            }

            Space space;
            if (this.spaceLexer.TryRead(scanner, out space))
            {
                element = new EscapedCharacter(space, context);
                return true;
            }

            VisibleCharacter visibleCharacter;
            if (this.visibleCharacterLexer.TryRead(scanner, out visibleCharacter))
            {
                element = new EscapedCharacter(visibleCharacter, context);
                return true;
            }

            ObsoletedText obsoletedText;
            if (this.obsoletedTextLexer.TryRead(scanner, out obsoletedText))
            {
                element = new EscapedCharacter(obsoletedText, context);
                return true;
            }

            element = default(EscapedCharacter);
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