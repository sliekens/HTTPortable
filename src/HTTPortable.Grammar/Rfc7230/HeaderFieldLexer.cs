namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;

    using SLANG;

    public class HeaderFieldLexer : Lexer<HeaderField>
    {
        private readonly ILexer<FieldName> fieldNameLexer;
        private readonly ILexer<FieldValue> fieldValueLexer;
        private readonly ILexer<OptionalWhiteSpace> optionalWhiteSpaceLexer;

        public HeaderFieldLexer()
            : this(new FieldNameLexer(), new OptionalWhiteSpaceLexer(), new FieldValueLexer())
        {
        }

        public HeaderFieldLexer(ILexer<FieldName> fieldNameLexer, ILexer<OptionalWhiteSpace> optionalWhiteSpaceLexer, 
            ILexer<FieldValue> fieldValueLexer)
            : base("header-field")
        {
            Contract.Requires(fieldNameLexer != null);
            Contract.Requires(optionalWhiteSpaceLexer != null);
            Contract.Requires(fieldValueLexer != null);
            this.fieldNameLexer = fieldNameLexer;
            this.optionalWhiteSpaceLexer = optionalWhiteSpaceLexer;
            this.fieldValueLexer = fieldValueLexer;
        }

        public override bool TryRead(ITextScanner scanner, out HeaderField element)
        {
            if (scanner.EndOfInput)
            {
                element = default(HeaderField);
                return false;
            }

            var context = scanner.GetContext();
            FieldName fieldName;
            if (!this.fieldNameLexer.TryRead(scanner, out fieldName))
            {
                element = default(HeaderField);
                return false;
            }

            if (!scanner.TryMatch(':'))
            {
                scanner.PutBack(fieldName.Data);
                element = default(HeaderField);
                return false;
            }

            OptionalWhiteSpace optionalWhiteSpace1;
            if (!this.optionalWhiteSpaceLexer.TryRead(scanner, out optionalWhiteSpace1))
            {
                scanner.PutBack(':');
                scanner.PutBack(fieldName.Data);
                element = default(HeaderField);
                return false;
            }

            FieldValue fieldValue;
            if (!this.fieldValueLexer.TryRead(scanner, out fieldValue))
            {
                scanner.PutBack(optionalWhiteSpace1.Data);
                scanner.PutBack(':');
                scanner.PutBack(fieldName.Data);
                element = default(HeaderField);
                return false;
            }

            OptionalWhiteSpace optionalWhiteSpace2;
            if (!this.optionalWhiteSpaceLexer.TryRead(scanner, out optionalWhiteSpace2))
            {
                scanner.PutBack(fieldValue.Data);
                scanner.PutBack(optionalWhiteSpace1.Data);
                scanner.PutBack(':');
                scanner.PutBack(fieldName.Data);
                element = default(HeaderField);
                return false;
            }

            element = new HeaderField(fieldName, optionalWhiteSpace1, fieldValue, optionalWhiteSpace2, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.fieldNameLexer != null);
            Contract.Invariant(this.optionalWhiteSpaceLexer != null);
            Contract.Invariant(this.fieldValueLexer != null);
        }
    }
}