namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;
    using SLANG;
    using Uri.Grammar;

    public class AbsoluteForm : Element
    {
        public AbsoluteForm(AbsoluteUri absoluteUri, ITextContext context)
            : base(absoluteUri.Data, context)
        {
            Contract.Requires(absoluteUri != null);
            Contract.Requires(context != null);
        }
    }
}