using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class HeaderFieldToken : Element
    {
        private readonly FieldNameToken fieldName;
        private readonly FieldValueToken fieldValue;

        public HeaderFieldToken(FieldNameToken fieldName, OWSToken ows1, FieldValueToken fieldValue, OWSToken ows2, ITextContext context)
            : base(string.Concat(fieldName.Data, ":", ows1.Data, fieldValue.Data, ows2.Data), context)
        {
            Contract.Requires(fieldName != null);
            Contract.Requires(ows1 != null);
            Contract.Requires(fieldValue != null);
            Contract.Requires(ows2 != null);
            this.fieldName = fieldName;
            this.fieldValue = fieldValue;
        }

        public FieldNameToken FieldName
        {
            get { return fieldName; }
        }

        public FieldValueToken FieldValue
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