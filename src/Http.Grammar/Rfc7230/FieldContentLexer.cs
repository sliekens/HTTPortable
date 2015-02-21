using System;
using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class FieldContentLexer : Lexer<FieldContent>
    {
        private readonly ILexer<FieldVChar> fieldVCharLexer;
        private readonly ILexer<RWS> rwsLexer;

        public FieldContentLexer()
            : this(new FieldVCharLexer(), new RWSLexer())
        {
        }

        public FieldContentLexer(ILexer<FieldVChar> fieldVCharLexer, ILexer<RWS> rwsLexer)
        {
            Contract.Requires(fieldVCharLexer != null);
            Contract.Requires(rwsLexer != null);
            this.fieldVCharLexer = fieldVCharLexer;
            this.rwsLexer = rwsLexer;
        }

        public override FieldContent Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            FieldContent element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(context, "Expected 'field-content'");
        }

        public override bool TryRead(ITextScanner scanner, out FieldContent element)
        {
            if (scanner.EndOfInput)
            {
                element = default(FieldContent);
                return false;
            }

            var context = scanner.GetContext();
            FieldVChar fieldVCharLeft;
            if (!this.fieldVCharLexer.TryRead(scanner, out fieldVCharLeft))
            {
                element = default(FieldContent);
                return false;
            }

            RWS rws;
            if (this.rwsLexer.TryRead(scanner, out rws))
            {
                FieldVChar fieldVCharRight;
                if (this.fieldVCharLexer.TryRead(scanner, out fieldVCharRight))
                {
                    element = new FieldContent(fieldVCharLeft, rws, fieldVCharRight, context);
                    return true;
                }

                this.rwsLexer.PutBack(scanner, rws);
            }

            element = new FieldContent(fieldVCharLeft, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.fieldVCharLexer != null);
            Contract.Invariant(this.rwsLexer != null);
        }

    }
}
