using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class FieldName : Element
    {
        public FieldName(Token element, ITextContext context)
            : base(element.Data, context)
        {
            Contract.Requires(element != null);
        }
    }
}
