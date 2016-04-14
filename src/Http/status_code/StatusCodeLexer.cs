using System;
using Txt;
using Txt.ABNF;

namespace Http.status_code
{
    public sealed class StatusCodeLexer : Lexer<StatusCode>
    {
        private readonly ILexer<Repetition> innerLexer;

        public StatusCodeLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<StatusCode> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<StatusCode>.FromResult(new StatusCode(result.Element));
            }
            return ReadResult<StatusCode>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}