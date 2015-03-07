namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;

    using Text.Scanning;

    public class BadWhiteSpaceLexer : Lexer<BadWhiteSpace>
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

        public override bool TryRead(ITextScanner scanner, out BadWhiteSpace element)
        {
            var context = scanner.GetContext();
            OptionalWhiteSpace optionalWhiteSpace;
            if (this.optionalWhiteSpaceLexer.TryRead(scanner, out optionalWhiteSpace))
            {
                element = new BadWhiteSpace(optionalWhiteSpace, context);
                return true;
            }

            element = default(BadWhiteSpace);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.optionalWhiteSpaceLexer != null);
        }
    }
}