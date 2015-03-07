namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;

    using Text.Scanning;
    using Text.Scanning.Core;

    public class QuotedTextLexer : Lexer<QuotedText>
    {
        private readonly ILexer<HorizontalTab> horizontalTabLexer;

        private readonly ILexer<Space> spaceLexer;

        public QuotedTextLexer()
            : this(new HorizontalTabLexer(), new SpaceLexer())
        {
        }

        public QuotedTextLexer(ILexer<HorizontalTab> horizontalTabLexer, ILexer<Space> spaceLexer)
            : base("qdtext")
        {
            Contract.Requires(horizontalTabLexer != null);
            Contract.Requires(spaceLexer != null);
            this.horizontalTabLexer = horizontalTabLexer;
            this.spaceLexer = spaceLexer;
        }

        public override bool TryRead(ITextScanner scanner, out QuotedText element)
        {
            if (scanner.EndOfInput)
            {
                element = default(QuotedText);
                return false;
            }

            var context = scanner.GetContext();
            HorizontalTab horizontalTab;
            if (this.horizontalTabLexer.TryRead(scanner, out horizontalTab))
            {
                element = QuotedText.Create1(horizontalTab, context);
                return true;
            }

            Space space;
            if (this.spaceLexer.TryRead(scanner, out space))
            {
                element = QuotedText.Create2(space, context);
                return true;
            }

            char c = '!';
            if (scanner.TryMatch(c))
            {
                element = QuotedText.Create3(c, context);
                return true;
            }

            for (c = '\x0023'; c < '\x005B'; c++)
            {
                if (scanner.TryMatch(c))
                {
                    element = QuotedText.Create4(c, context);
                    return true;
                }
            }

            for (c = '\x005D'; c < '\x007E'; c++)
            {
                if (scanner.TryMatch(c))
                {
                    element = QuotedText.Create5(c, context);
                    return true;
                }
            }

            element = default(QuotedText);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.horizontalTabLexer != null);
            Contract.Invariant(this.spaceLexer != null);
        }
    }
}