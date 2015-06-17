namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;

    using SLANG;

    public class HttpName : Element
    {
        public HttpName(Element element, ITextContext context)
            : base(element.Data, context)
        {
            Contract.Requires(element != null);
            Contract.Requires(context != null);
        }
    }
}