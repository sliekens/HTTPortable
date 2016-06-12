using System;
using Txt;
using Txt.ABNF;
using Txt.Core;

namespace Http.token
{
    public sealed class TokenLexer : Lexer<Token>
    {
        private readonly ILexer<Repetition> innerLexer;

        public TokenLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<Token> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<Token>.FromResult(new Token(result.Element));
            }
            return ReadResult<Token>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}