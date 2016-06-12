using System;
using Txt;
using Txt.ABNF;
using Txt.Core;

namespace Http.reason_phrase
{
    public sealed class ReasonPhraseLexer : Lexer<ReasonPhrase>
    {
        private readonly ILexer<Repetition> innerLexer;

        public ReasonPhraseLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<ReasonPhrase> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<ReasonPhrase>.FromResult(new ReasonPhrase(result.Element));
            }
            return ReadResult<ReasonPhrase>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
