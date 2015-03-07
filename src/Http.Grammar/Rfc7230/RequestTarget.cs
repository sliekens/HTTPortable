namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;

    using Text.Scanning;

    public class RequestTarget : Element
    {
        public RequestTarget(OriginForm originForm, ITextContext context)
            : base(originForm.Data, context)
        {
            Contract.Requires(originForm != null);
            Contract.Requires(context != null);
        }

        public RequestTarget(AbsoluteForm absoluteForm, ITextContext context)
            : base(absoluteForm.Data, context)
        {
            Contract.Requires(absoluteForm != null);
            Contract.Requires(context != null);
        }

        public RequestTarget(AuthorityForm authorityForm, ITextContext context)
            : base(authorityForm.Data, context)
        {
            Contract.Requires(authorityForm != null);
            Contract.Requires(context != null);
        }

        public RequestTarget(AsteriskForm asteriskForm, ITextContext context)
            : base(asteriskForm.Data, context)
        {
            Contract.Requires(asteriskForm != null);
            Contract.Requires(context != null);
        }
    }
}
