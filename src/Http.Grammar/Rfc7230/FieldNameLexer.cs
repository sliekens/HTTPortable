using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class FieldNameLexer : Lexer<FieldNameToken>
    {
        private readonly ILexer<Token> tokenLexer;

        public FieldNameLexer()
            : this(new TokenLexer())
        {
        }

        public FieldNameLexer(ILexer<Token> tokenLexer)
        {
            Contract.Requires(tokenLexer != null);
            this.tokenLexer = tokenLexer;
        }

        public override FieldNameToken Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            Token token;
            try
            {
                token = this.tokenLexer.Read(scanner);
            }
            catch (SyntaxErrorException syntaxErrorException)
            {
                throw new SyntaxErrorException(context, "Expected 'field-name'", syntaxErrorException);

            }

            return new FieldNameToken(token, context);
        }

        public override bool TryRead(ITextScanner scanner, out FieldNameToken element)
        {
            if (scanner.EndOfInput)
            {
                element = default(FieldNameToken);
                return false;
            }

            var context = scanner.GetContext();
            Token token;
            if (this.tokenLexer.TryRead(scanner, out token))
            {
                element = new FieldNameToken(token, context);
                return true;
            }

            element = default(FieldNameToken);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.tokenLexer != null);
        }

    }
}
