namespace Http.Grammar.Rfc7230
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    using Text.Scanning;
    using Text.Scanning.Core;

    public class TrailerPartLexer : Lexer<TrailerPart>
    {
        private readonly ILexer<HeaderField> headerFieldLexer;

        private readonly ILexer<EndOfLine> endOfLineLexer;

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
            if (scanner.EndOfInput)
            {
                element = default(TrailerPart);
                return false;
            }

            var context = scanner.GetContext();
            var headerFields = new List<HeaderField>();
            HeaderField headerField;
            while (this.headerFieldLexer.TryRead(scanner, out headerField))
            {
                headerFields.Add(headerField);
            }

            if (headerFields.Count == 0)
            {
                element = default(TrailerPart);
                return false;
            }

            EndOfLine endOfLine;
            if (this.endOfLineLexer.TryRead(scanner, out endOfLine))
            {
                element = new TrailerPart(headerFields, endOfLine, context);
                return true;
            }

            for (int i = headerFields.Count - 1; i >= 0; i--)
            {
                scanner.PutBack(headerFields[i].Data);
            }

            element = default(TrailerPart);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.headerFieldLexer != null);
            Contract.Invariant(this.endOfLineLexer != null);
        }
    }
}
