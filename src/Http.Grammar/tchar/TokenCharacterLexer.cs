namespace Http.Grammar
{
    using System;
    using TextFx;
    using TextFx.ABNF;

    public sealed class TokenCharacterLexer : Lexer<TokenCharacter>
    {
        private readonly ILexer<Alternative> innerLexer;

        public TokenCharacterLexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<TokenCharacter> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<TokenCharacter>.FromResult(new TokenCharacter(result.Element));
            }
            return ReadResult<TokenCharacter>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}