using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class HeaderFieldLexer : Lexer<HeaderFieldToken>
    {
        private readonly ILexer<FieldNameToken> fieldNameLexer;
        private readonly ILexer<OWSToken> owsLexer;
        private readonly ILexer<FieldValueToken> fieldValueLexer;

        public HeaderFieldLexer()
            : this(new FieldNameLexer(), new OWSLexer(), new FieldValueLexer())
        {
        }

        public HeaderFieldLexer(ILexer<FieldNameToken> fieldNameLexer, ILexer<OWSToken> owsLexer, ILexer<FieldValueToken> fieldValueLexer)
        {
            Contract.Requires(fieldNameLexer != null);
            Contract.Requires(owsLexer != null);
            Contract.Requires(fieldValueLexer != null);
            this.fieldNameLexer = fieldNameLexer;
            this.owsLexer = owsLexer;
            this.fieldValueLexer = fieldValueLexer;
        }

        public override HeaderFieldToken Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            HeaderFieldToken element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(context, "Expected 'header-field'");
        }

        public override bool TryRead(ITextScanner scanner, out HeaderFieldToken element)
        {
            if (scanner.EndOfInput)
            {
                element = default(HeaderFieldToken);
                return false;
            }

            var context = scanner.GetContext();
            FieldNameToken fieldName;
            if (!this.fieldNameLexer.TryRead(scanner, out fieldName))
            {
                element = default(HeaderFieldToken);
                return false;
            }

            if (!scanner.TryMatch(':'))
            {
                this.fieldNameLexer.PutBack(scanner, fieldName);
                element = default(HeaderFieldToken);
                return false;
            }

            OWSToken ows1;
            if (!this.owsLexer.TryRead(scanner, out ows1))
            {
                scanner.PutBack(':');
                this.fieldNameLexer.PutBack(scanner, fieldName);
                element = default(HeaderFieldToken);
                return false;
            }

            FieldValueToken fieldValue;
            if (!this.fieldValueLexer.TryRead(scanner, out fieldValue))
            {
                this.owsLexer.PutBack(scanner, ows1);
                scanner.PutBack(':');
                this.fieldNameLexer.PutBack(scanner, fieldName);
                element = default(HeaderFieldToken);
                return false;
            }

            OWSToken ows2;
            if (!this.owsLexer.TryRead(scanner, out ows2))
            {
                this.fieldValueLexer.PutBack(scanner, fieldValue);
                this.owsLexer.PutBack(scanner, ows1);
                scanner.PutBack(':');
                this.fieldNameLexer.PutBack(scanner, fieldName);
                element = default(HeaderFieldToken);
                return false;
            }

            element = new HeaderFieldToken(fieldName, ows1, fieldValue, ows2, context);
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
