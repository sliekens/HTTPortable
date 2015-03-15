namespace Http.Grammar.Rfc7230
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using SLANG;
    using SLANG.Core;

    public class RequiredWhiteSpaceLexer : RepetitionLexer<RequiredWhiteSpace, WhiteSpace>
    {
        private readonly ILexer<WhiteSpace> whiteSpaceLexer;

        public RequiredWhiteSpaceLexer()
            : this(new WhiteSpaceLexer())
        {
        }

        public RequiredWhiteSpaceLexer(ILexer<WhiteSpace> whiteSpaceLexer)
            : base("RWS", 1, int.MaxValue)
        {
            Contract.Requires(whiteSpaceLexer != null);
            this.whiteSpaceLexer = whiteSpaceLexer;
        }

        protected override RequiredWhiteSpace CreateInstance(IList<WhiteSpace> elements, int lowerBound, int upperBound, ITextContext context)
        {
            return new RequiredWhiteSpace(elements, context);
        }

        protected override bool TryReadOne(ITextScanner scanner, out WhiteSpace element)
        {
            return this.whiteSpaceLexer.TryRead(scanner, out element);
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.whiteSpaceLexer != null);
        }
    }
}