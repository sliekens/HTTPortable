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
            FieldContentToken token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(context, "Expected 'field-content'");
        }

        public override bool TryRead(ITextScanner scanner, out FieldContentToken token)
        {
            if (scanner.EndOfInput)
            {
                token = default(FieldContentToken);
                return false;
            }

            var context = scanner.GetContext();
            FieldVCharToken fieldVCharLeft;
            if (!this.fieldVCharLexer.TryRead(scanner, out fieldVCharLeft))
            {
                token = default(FieldContentToken);
                return false;
            }

            RWSToken rws;
            if (this.rwsLexer.TryRead(scanner, out rws))
            {
                FieldVCharToken fieldVCharRight;
                if (this.fieldVCharLexer.TryRead(scanner, out fieldVCharRight))
                {
                    token = new FieldContentToken(fieldVCharLeft, rws, fieldVCharRight, context);
                    return true;
                }

                this.rwsLexer.PutBack(scanner, rws);
            }

            token = new FieldContentToken(fieldVCharLeft, context);
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
