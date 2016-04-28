using System;
using Txt;
using Txt.ABNF;

namespace Http.tchar
{
    public sealed class TokenCharacterLexer : Lexer<TokenCharacter>
    {
        private readonly ILexer<Alternation> innerLexer;

        public TokenCharacterLexer(ILexer<Alternation> innerLexer)
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