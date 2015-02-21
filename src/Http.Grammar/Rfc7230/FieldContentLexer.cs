using System;
using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class FieldContentLexer : Lexer<FieldContentToken>
    {
        private readonly ILexer<FieldVCharToken> fieldVCharLexer;
        private readonly ILexer<RWSToken> rwsLexer;

        public FieldContentLexer()
            : this(new FieldVCharLexer(), new RWSLexer())
        {
        }

        public FieldContentLexer(ILexer<FieldVCharToken> fieldVCharLexer, ILexer<RWSToken> rwsLexer)
        {
            Contract.Requires(fieldVCharLexer != null);
            Contract.Requires(rwsLexer != null);
            this.fieldVCharLexer = fieldVCharLexer;
            this.rwsLexer = rwsLexer;
        }

        public override FieldContentToken Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            FieldContentToken element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(context, "Expected 'field-content'");
        }

        public override bool TryRead(ITextScanner scanner, out FieldContentToken element)
        {
            if (scanner.EndOfInput)
            {
                element = default(FieldContentToken);
                return false;
            }

            var context = scanner.GetContext();
            FieldVCharToken fieldVCharLeft;
            if (!this.fieldVCharLexer.TryRead(scanner, out fieldVCharLeft))
            {
                element = default(FieldContentToken);
                return false;
            }

            RWSToken rws;
            if (this.rwsLexer.TryRead(scanner, out rws))
            {
                FieldVCharToken fieldVCharRight;
                if (this.fieldVCharLexer.TryRead(scanner, out fieldVCharRight))
                {
                    element = new FieldContentToken(fieldVCharLeft, rws, fieldVCharRight, context);
                    return true;
                }

                this.rwsLexer.PutBack(scanner, rws);
            }

            element = new FieldContentToken(fieldVCharLeft, context);
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
