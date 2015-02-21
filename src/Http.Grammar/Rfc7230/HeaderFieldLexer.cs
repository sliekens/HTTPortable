using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class HeaderFieldLexer : Lexer<HeaderField>
    {
        private readonly ILexer<FieldName> fieldNameLexer;
        private readonly ILexer<OptionalWhiteSpace> optionalWhiteSpaceLexer;
        private readonly ILexer<FieldValue> fieldValueLexer;

        public HeaderFieldLexer()
            : this(new FieldNameLexer(), new OptionalWhiteSpaceLexer(), new FieldValueLexer())
        {
        }

        public HeaderFieldLexer(ILexer<FieldName> fieldNameLexer, ILexer<OptionalWhiteSpace> optionalWhiteSpaceLexer, ILexer<FieldValue> fieldValueLexer)
        {
            Contract.Requires(fieldNameLexer != null);
            Contract.Requires(optionalWhiteSpaceLexer != null);
            Contract.Requires(fieldValueLexer != null);
            this.fieldNameLexer = fieldNameLexer;
            this.optionalWhiteSpaceLexer = optionalWhiteSpaceLexer;
            this.fieldValueLexer = fieldValueLexer;
        }

        public override HeaderField Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            HeaderField element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(context, "Expected 'header-field'");
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
                this.fieldNameLexer.PutBack(scanner, fieldName);
                element = default(HeaderField);
                return false;
            }

            OptionalWhiteSpace optionalWhiteSpace1;
            if (!this.optionalWhiteSpaceLexer.TryRead(scanner, out optionalWhiteSpace1))
            {
                scanner.PutBack(':');
                this.fieldNameLexer.PutBack(scanner, fieldName);
                element = default(HeaderField);
                return false;
            }

            FieldValue fieldValue;
            if (!this.fieldValueLexer.TryRead(scanner, out fieldValue))
            {
                this.optionalWhiteSpaceLexer.PutBack(scanner, optionalWhiteSpace1);
                scanner.PutBack(':');
                this.fieldNameLexer.PutBack(scanner, fieldName);
                element = default(HeaderField);
                return false;
            }

            OptionalWhiteSpace optionalWhiteSpace2;
            if (!this.optionalWhiteSpaceLexer.TryRead(scanner, out optionalWhiteSpace2))
            {
                this.fieldValueLexer.PutBack(scanner, fieldValue);
                this.optionalWhiteSpaceLexer.PutBack(scanner, optionalWhiteSpace1);
                scanner.PutBack(':');
                this.fieldNameLexer.PutBack(scanner, fieldName);
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
