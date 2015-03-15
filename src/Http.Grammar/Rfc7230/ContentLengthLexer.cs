namespace Http.Grammar.Rfc7230
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using SLANG;
    using SLANG.Core;

    public class ContentLengthLexer : RepetitionLexer<ContentLength, Digit>
    {
        private readonly ILexer<Digit> digitLexer;

        public ContentLengthLexer()
            : this(new DigitLexer())
        {
        }

        public ContentLengthLexer(ILexer<Digit> digitLexer)
            : base("Content-Length", 1, int.MaxValue)
        {
            Contract.Requires(digitLexer != null);
            this.digitLexer = digitLexer;
        }

        protected override ContentLength CreateInstance(IList<Digit> elements, int lowerBound, int upperBound, ITextContext context)
        {
            return new ContentLength(elements, context);
        }

        protected override bool TryRead(ITextScanner scanner, out Digit element)
        {
            return this.digitLexer.TryRead(scanner, out element);
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.digitLexer != null);
        }
    }
}