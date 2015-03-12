namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;
    using SLANG;

    public class BadWhiteSpaceLexer : Lexer<OptionalWhiteSpace>
    {
        private readonly ILexer<OptionalWhiteSpace> optionalWhiteSpaceLexer;

        public BadWhiteSpaceLexer()
            : this(new OptionalWhiteSpaceLexer())
        {
        }

        public BadWhiteSpaceLexer(ILexer<OptionalWhiteSpace> optionalWhiteSpaceLexer)
            : base("BWS")
        {
            Contract.Requires(optionalWhiteSpaceLexer != null);
            this.optionalWhiteSpaceLexer = optionalWhiteSpaceLexer;
        }

        public override bool TryRead(ITextScanner scanner, out OptionalWhiteSpace element)
        {
            if (this.optionalWhiteSpaceLexer.TryRead(scanner, out element))
            {
                return true;
            }

            element = default(OptionalWhiteSpace);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.optionalWhiteSpaceLexer != null);
        }
    }
}