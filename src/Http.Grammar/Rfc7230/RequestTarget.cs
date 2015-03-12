namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;
    using SLANG;
    using Uri.Grammar;

    public class RequestTarget : Alternative<OriginForm, AbsoluteUri, Authority, AsteriskForm>
    {
        public RequestTarget(OriginForm element, ITextContext context)
            : base(element, context)
        {
            Contract.Requires(element != null);
            Contract.Requires(context != null);
        }

        public RequestTarget(AbsoluteUri element, ITextContext context)
            : base(element, context)
        {
            Contract.Requires(element != null);
            Contract.Requires(context != null);
        }

        public RequestTarget(Authority element, ITextContext context)
            : base(element, context)
        {
            Contract.Requires(element != null);
            Contract.Requires(context != null);
        }

        public RequestTarget(AsteriskForm element, ITextContext context)
            : base(element, context)
        {
            Contract.Requires(element != null);
            Contract.Requires(context != null);
        }
    }
}