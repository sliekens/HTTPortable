namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;

    using Text.Scanning;

    public class RequestTargetLexer : Lexer<RequestTarget>
    {
        private readonly ILexer<OriginForm> originFormLexer;

        private readonly ILexer<AbsoluteForm> absoluteFormLexer;

        private readonly ILexer<AuthorityForm> authorityFormLexer;

        private readonly ILexer<AsteriskForm> asteriskFormLexer;

        public RequestTargetLexer()
            : this(new OriginFormLexer(), new AbsoluteFormLexer(), new AuthorityFormLexer(), new AsteriskFormLexer())
        {
        }

        public RequestTargetLexer(ILexer<OriginForm> originFormLexer, ILexer<AbsoluteForm> absoluteFormLexer, ILexer<AuthorityForm> authorityFormLexer, ILexer<AsteriskForm> asteriskFormLexer)
            : base("request-target")
        {
            Contract.Requires(originFormLexer != null);
            Contract.Requires(absoluteFormLexer != null);
            Contract.Requires(authorityFormLexer != null);
            Contract.Requires(asteriskFormLexer != null);
            this.originFormLexer = originFormLexer;
            this.absoluteFormLexer = absoluteFormLexer;
            this.authorityFormLexer = authorityFormLexer;
            this.asteriskFormLexer = asteriskFormLexer;
        }

        public override bool TryRead(ITextScanner scanner, out RequestTarget element)
        {
            if (scanner.EndOfInput)
            {
                element = default(RequestTarget);
                return false;
            }

            var context = scanner.GetContext();
            OriginForm originForm;
            if (this.originFormLexer.TryRead(scanner, out originForm))
            {
                element = new RequestTarget(originForm, context);
                return true;
            }

            AbsoluteForm absoluteForm;
            if (this.absoluteFormLexer.TryRead(scanner, out absoluteForm))
            {
                element = new RequestTarget(absoluteForm, context);
                return true;
            }

            AuthorityForm authorityForm;
            if (this.authorityFormLexer.TryRead(scanner, out authorityForm))
            {
                element = new RequestTarget(authorityForm, context);
                return true;
            }

            AsteriskForm asteriskForm;
            if (this.asteriskFormLexer.TryRead(scanner, out asteriskForm))
            {
                element = new RequestTarget(asteriskForm, context);
                return true;
            }

            element = default(RequestTarget);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.originFormLexer != null);
            Contract.Invariant(this.absoluteFormLexer != null);
            Contract.Invariant(this.authorityFormLexer != null);
            Contract.Invariant(this.asteriskFormLexer != null);
        }
    }
}