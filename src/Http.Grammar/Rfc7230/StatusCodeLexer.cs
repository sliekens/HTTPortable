namespace Http.Grammar.Rfc7230
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using SLANG;
    using SLANG.Core;

    public class StatusCodeLexer : RepetitionLexer<StatusCode, Digit>
    {
        private readonly ILexer<Digit> digitLexer;

        public StatusCodeLexer()
            : this(new DigitLexer())
        {
        }

        public StatusCodeLexer(ILexer<Digit> digitLexer)
            : base("status-code", 3, 3)
        {
            Contract.Requires(digitLexer != null);
            this.digitLexer = digitLexer;
        }

        protected override StatusCode CreateInstance(IList<Digit> elements, int lowerBound, int upperBound, ITextContext context)
        {
            return new StatusCode(elements, context);
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