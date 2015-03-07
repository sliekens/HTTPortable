namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;

    using Text.Scanning;

    using Uri.Grammar;

    public class OriginForm : Element
    {
        public OriginForm(AbsolutePath absolutePath, Element querySeparator, Query query, ITextContext context)
            : base(string.Concat(absolutePath, querySeparator, query), context)
        {
            Contract.Requires(absolutePath != null);
            Contract.Requires(querySeparator == null || (querySeparator.Data == "?" && query != null));
            Contract.Requires(context != null);
        }
    }
}
