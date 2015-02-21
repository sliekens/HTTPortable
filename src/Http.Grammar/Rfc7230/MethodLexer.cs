using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class MethodLexer : Lexer<MethodToken>
    {
        private readonly ILexer<Token> tokenLexer;

        public MethodLexer()
            : this(new TokenLexer())
        {
        }

        public MethodLexer(ILexer<Token> tokenLexer)
        {
            Contract.Requires(tokenLexer != null);
            this.tokenLexer = tokenLexer;
        }

        public override MethodToken Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            MethodToken element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(context, "Expected 'method'");
        }

        public override bool TryRead(ITextScanner scanner, out MethodToken element)
        {
            var context = scanner.GetContext();
            Token token;
            if (this.tokenLexer.TryRead(scanner, out token))
            {
                element = new MethodToken(token, context);
                return true;
            }

            element = default(MethodToken);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.tokenLexer != null);
        }
    }
}
