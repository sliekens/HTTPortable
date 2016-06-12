using System;
using Txt;
using Txt.ABNF;
using Txt.Core;

namespace Http.status_line
{
    public sealed class StatusLineLexer : Lexer<StatusLine>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public StatusLineLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<StatusLine> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<StatusLine>.FromResult(new StatusLine(result.Element));
            }
            return ReadResult<StatusLine>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
