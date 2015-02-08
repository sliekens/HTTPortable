using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class FieldNameLexer : Lexer<FieldNameToken>
    {
        private readonly ILexer<TokenToken> tokenLexer;

        public FieldNameLexer()
            : this(new TokenLexer())
        {
        }

        public FieldNameLexer(ILexer<TokenToken> tokenLexer)
        {
            Contract.Requires(tokenLexer != null);
            this.tokenLexer = tokenLexer;
        }

        public override FieldNameToken Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            TokenToken tokenToken;
            try
            {
                tokenToken = this.tokenLexer.Read(scanner);
            }
            catch (SyntaxErrorException syntaxErrorException)
            {
                throw new SyntaxErrorException(context, "Expected 'field-name'", syntaxErrorException);

            }

            return new FieldNameToken(tokenToken, context);
        }

        public override bool TryRead(ITextScanner scanner, out FieldNameToken token)
        {
            if (scanner.EndOfInput)
            {
                token = default(FieldNameToken);
                return false;
            }

            var context = scanner.GetContext();
            TokenToken tokenToken;
            if (this.tokenLexer.TryRead(scanner, out tokenToken))
            {
                token = new FieldNameToken(tokenToken, context);
                return true;
            }

            token = default(FieldNameToken);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.tokenLexer != null);
        }

    }
}
