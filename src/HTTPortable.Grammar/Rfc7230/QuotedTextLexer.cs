namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;

    using SLANG;
    using SLANG.Core;

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
                element = new QuotedText(horizontalTab, context);
                return true;
            }

            Space space;
            if (this.spaceLexer.TryRead(scanner, out space))
            {
                element = new QuotedText(space, context);
                return true;
            }


            Element exclamation;
            if (TryReadTerminal(scanner, "!", out exclamation))
            {
                element = new QuotedText(exclamation, context);
                return true;
            }

            for (char c = '\x23'; c < '\x5B'; c++)
            {
                Element terminal;
                if (TryReadTerminal(scanner, c, out terminal))
                {
                    element = new QuotedText(terminal, context);
                    return true;
                }
            }

            for (char c = '\x5D'; c < '\x7E'; c++)
            {
                Element terminal;
                if (TryReadTerminal(scanner, c, out terminal))
                {
                    element = new QuotedText(terminal, context);
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