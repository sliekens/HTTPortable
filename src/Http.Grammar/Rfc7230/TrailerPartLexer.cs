namespace Http.Grammar.Rfc7230
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using SLANG;
    using SLANG.Core;
    using HeaderLine = SLANG.Sequence<HeaderField, SLANG.Core.EndOfLine>;

    public class TrailerPartLexer : Lexer<TrailerPart>
    {
        private readonly ILexer<EndOfLine> endOfLineLexer;
        private readonly ILexer<HeaderField> headerFieldLexer;

        public TrailerPartLexer(ILexer<HeaderField> headerFieldLexer, ILexer<EndOfLine> endOfLineLexer)
            : base("trailer-part")
        {
            Contract.Requires(headerFieldLexer != null);
            Contract.Requires(endOfLineLexer != null);
            this.headerFieldLexer = headerFieldLexer;
            this.endOfLineLexer = endOfLineLexer;
        }

        public override bool TryRead(ITextScanner scanner, out TrailerPart element)
        {
            var context = scanner.GetContext();
            var elements = new List<HeaderLine>();
            HeaderLine headerLine;
            while (this.TryReadHeaderLine(scanner, out headerLine))
            {
                elements.Add(headerLine);
            }

            element = new TrailerPart(elements, context);
            return true;
        }

        private bool TryReadHeaderLine(ITextScanner scanner, out HeaderLine element)
        {
            if (scanner.EndOfInput)
            {
                element = default(HeaderLine);
                return false;
            }

            var context = scanner.GetContext();
            HeaderField headerField;
            if (!this.headerFieldLexer.TryRead(scanner, out headerField))
            {
                element = default(HeaderLine);
                return false;
            }

            EndOfLine endOfLine;
            if (!this.endOfLineLexer.TryRead(scanner, out endOfLine))
            {
                scanner.PutBack(headerField.Data);
                element = default(HeaderLine);
                return false;
            }

            element = new HeaderLine(headerField, endOfLine, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.headerFieldLexer != null);
            Contract.Invariant(this.endOfLineLexer != null);
        }
    }
}