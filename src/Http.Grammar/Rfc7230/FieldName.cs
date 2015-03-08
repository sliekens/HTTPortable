using System.Diagnostics.Contracts;


namespace Http.Grammar.Rfc7230
{
    using SLANG;

    public class FieldName : Element
    {
        public FieldName(Token element, ITextContext context)
            : base(element.Data, context)
        {
            Contract.Requires(element != null);
        }
    }
}
