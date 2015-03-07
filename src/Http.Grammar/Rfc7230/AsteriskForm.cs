namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;

    using Text.Scanning;

    public class AsteriskForm : Element
    {
        public AsteriskForm(Element asterisk, ITextContext context)
            : base(asterisk.Data, context)
        {
            Contract.Requires(asterisk != null && asterisk.Data == "*");
            Contract.Requires(context != null);
        }
    }
}
