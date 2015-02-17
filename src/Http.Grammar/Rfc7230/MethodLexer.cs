using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class MethodLexer : Lexer<MethodToken>
    {
        private readonly ILexer<TokenToken> tokenLexer;

        public MethodLexer()
            : this(new TokenLexer())
        {
        }

        public MethodLexer(ILexer<TokenToken> tokenLexer)
        {
            Contract.Requires(tokenLexer != null);
            this.tokenLexer = tokenLexer;
        }

        public override MethodToken Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            MethodToken token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(context, "Expected 'method'");
        }

        public override bool TryRead(ITextScanner scanner, out MethodToken token)
        {
            var context = scanner.GetContext();
            TokenToken tokenToken;
            if (this.tokenLexer.TryRead(scanner, out tokenToken))
            {
                token = new MethodToken(tokenToken, context);
                return true;
            }

            token = default(MethodToken);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.tokenLexer != null);
        }
    }
}
