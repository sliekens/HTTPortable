namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;

    using SLANG;

    public class FieldName : Element
    {
        public FieldName(Token element, ITextContext context)
            : base(element.Data, context)
        {
            Contract.Requires(element != null);
            Contract.Requires(context != null);
        }
    }
}