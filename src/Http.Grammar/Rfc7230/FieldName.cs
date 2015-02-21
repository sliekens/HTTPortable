using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class FieldName : Element
    {
        private readonly Token element;

        public FieldName(Token element, ITextContext context)
            : base(element.Data, context)
        {
            Contract.Requires(element != null);
            this.element = element;
        }

        public Token Element
        {
            get
            {
                return this.element;
            }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.element != null);
        }
    }
}
