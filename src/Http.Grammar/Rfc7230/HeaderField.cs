using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class HeaderField : Element
    {
        private readonly FieldName fieldName;
        private readonly FieldValue fieldValue;

        public HeaderField(FieldName fieldName, OptionalWhiteSpace ows1, FieldValue fieldValue, OptionalWhiteSpace ows2, ITextContext context)
            : base(string.Concat(fieldName.Data, ":", ows1.Data, fieldValue.Data, ows2.Data), context)
        {
            Contract.Requires(fieldName != null);
            Contract.Requires(ows1 != null);
            Contract.Requires(fieldValue != null);
            Contract.Requires(ows2 != null);
            this.fieldName = fieldName;
            this.fieldValue = fieldValue;
        }

        public FieldName FieldName
        {
            get { return fieldName; }
        }

        public FieldValue FieldValue
        {
            get { return fieldValue; }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.fieldName != null);
            Contract.Invariant(this.fieldValue != null);
        }
    }
}