namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;
    using SLANG;
    using Uri.Grammar;
    using UriHost = Uri.Grammar.Host;

    public class Host : Sequence<UriHost, Option<Sequence<Element, Port>>>
    {
        public Host(UriHost element1, Option<Sequence<Element, Port>> element2, ITextContext context)
            : base(element1, element2, context)
        {
            Contract.Requires(element1 != null);
            Contract.Requires(element2 != null);
            Contract.Requires(context != null);
        }
    }
}