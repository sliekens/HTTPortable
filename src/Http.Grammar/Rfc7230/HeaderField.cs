﻿using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class HeaderField : Element
    {
        private readonly FieldName fieldName;
        private readonly FieldValue fieldValue;

        public HeaderField(FieldName fieldName, OptionalWhiteSpace optionalWhiteSpace1, FieldValue fieldValue, OptionalWhiteSpace optionalWhiteSpace2, ITextContext context)
            : base(string.Concat(fieldName.Data, ":", optionalWhiteSpace1.Data, fieldValue.Data, optionalWhiteSpace2.Data), context)
        {
            Contract.Requires(fieldName != null);
            Contract.Requires(optionalWhiteSpace1 != null);
            Contract.Requires(fieldValue != null);
            Contract.Requires(optionalWhiteSpace2 != null);
            this.fieldName = fieldName;
            this.fieldValue = fieldValue;
        }

        public FieldName FieldName
        {
            get
            {
                return this.fieldName;
            }
        }

        public FieldValue FieldValue
        {
            get
            {
                return this.fieldValue;
            }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.fieldName != null);
            Contract.Invariant(this.fieldValue != null);
        }
    }
}