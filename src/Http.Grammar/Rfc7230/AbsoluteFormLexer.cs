namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;
    using SLANG;
    using Uri.Grammar;

    public class AbsoluteFormLexer : Lexer<AbsoluteUri>
    {
        private readonly ILexer<AbsoluteUri> absoluteUriLexer;

        public AbsoluteFormLexer()
            : this(new AbsoluteUriLexer())
        {
        }

        public AbsoluteFormLexer(ILexer<AbsoluteUri> absoluteUriLexer)
            : base("absolute-form")
        {
            Contract.Requires(absoluteUriLexer != null);
            this.absoluteUriLexer = absoluteUriLexer;
        }

        public override bool TryRead(ITextScanner scanner, out AbsoluteUri element)
        {
            if (scanner.EndOfInput)
            {
                element = default(AbsoluteUri);
                return false;
            }

            if (this.absoluteUriLexer.TryRead(scanner, out element))
            {
                return true;
            }

            element = default(AbsoluteUri);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.absoluteUriLexer != null);
        }
    }
}