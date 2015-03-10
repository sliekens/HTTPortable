namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;
    using SLANG;

    public class HttpName : Element
    {
        public HttpName(ITextContext context)
            : base("HTTP", context)
        {
            Contract.Requires(context != null);
        }
    }
}