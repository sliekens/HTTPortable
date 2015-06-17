namespace Http.Grammar.Rfc7230
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    using SLANG;
    using SLANG.Core;

    using HeaderLine = SLANG.Sequence<HeaderField, SLANG.Core.EndOfLine>;

    public class TrailerPartLexer : RepetitionLexer<TrailerPart, HeaderLine>
    {
        private readonly ILexer<EndOfLine> endOfLineLexer;
        private readonly ILexer<HeaderField> headerFieldLexer;

        public TrailerPartLexer()
            : this(new HeaderFieldLexer(), new EndOfLineLexer())
        {
        }

        public TrailerPartLexer(ILexer<HeaderField> headerFieldLexer, ILexer<EndOfLine> endOfLineLexer)
            : base("trailer-part", 0, Int32.MaxValue)
        {
            Contract.Requires(headerFieldLexer != null);
            Contract.Requires(endOfLineLexer != null);
            this.headerFieldLexer = headerFieldLexer;
            this.endOfLineLexer = endOfLineLexer;
        }

        protected override TrailerPart CreateInstance(IList<HeaderLine> elements, int lowerBound, int upperBound, ITextContext context)
        {
            return new TrailerPart(elements, context);
        }

        protected override bool TryRead(ITextScanner scanner, int lowerBound, int upperBound, int current, out HeaderLine element)
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