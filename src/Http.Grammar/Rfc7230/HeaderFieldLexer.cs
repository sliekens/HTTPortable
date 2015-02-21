using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class HeaderFieldLexer : Lexer<HeaderField>
    {
        private readonly ILexer<FieldName> fieldNameLexer;
        private readonly ILexer<OptionalWhiteSpace> owsLexer;
        private readonly ILexer<FieldValue> fieldValueLexer;

        public HeaderFieldLexer()
            : this(new FieldNameLexer(), new OptionalWhiteSpaceLexer(), new FieldValueLexer())
        {
        }

        public HeaderFieldLexer(ILexer<FieldName> fieldNameLexer, ILexer<OptionalWhiteSpace> owsLexer, ILexer<FieldValue> fieldValueLexer)
        {
            Contract.Requires(fieldNameLexer != null);
            Contract.Requires(owsLexer != null);
            Contract.Requires(fieldValueLexer != null);
            this.fieldNameLexer = fieldNameLexer;
            this.owsLexer = owsLexer;
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

            OptionalWhiteSpace ows1;
            if (!this.owsLexer.TryRead(scanner, out ows1))
            {
                scanner.PutBack(':');
                this.fieldNameLexer.PutBack(scanner, fieldName);
                element = default(HeaderField);
                return false;
            }

            FieldValue fieldValue;
            if (!this.fieldValueLexer.TryRead(scanner, out fieldValue))
            {
                this.owsLexer.PutBack(scanner, ows1);
                scanner.PutBack(':');
                this.fieldNameLexer.PutBack(scanner, fieldName);
                element = default(HeaderField);
                return false;
            }

            OptionalWhiteSpace ows2;
            if (!this.owsLexer.TryRead(scanner, out ows2))
            {
                this.fieldValueLexer.PutBack(scanner, fieldValue);
                this.owsLexer.PutBack(scanner, ows1);
                scanner.PutBack(':');
                this.fieldNameLexer.PutBack(scanner, fieldName);
                element = default(HeaderField);
                return false;
            }

            element = new HeaderField(fieldName, ows1, fieldValue, ows2, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.fieldNameLexer != null);
            Contract.Invariant(this.owsLexer != null);
            Contract.Invariant(this.fieldValueLexer != null);
        }

    }
}
