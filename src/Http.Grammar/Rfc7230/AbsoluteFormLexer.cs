namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;
    using SLANG;
    using Uri.Grammar;

    public class AbsoluteFormLexer : Lexer<AbsoluteForm>
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

        public override bool TryRead(ITextScanner scanner, out AbsoluteForm element)
        {
            if (scanner.EndOfInput)
            {
                element = default(AbsoluteForm);
                return false;
            }

            var context = scanner.GetContext();
            AbsoluteUri absoluteUri;
            if (this.absoluteUriLexer.TryRead(scanner, out absoluteUri))
            {
                element = new AbsoluteForm(absoluteUri, context);
                return true;
            }

            element = default(AbsoluteForm);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.absoluteUriLexer != null);
        }
    }
}