using System;
using Txt;
using Txt.ABNF;

namespace Http.request_line
{
    public sealed class RequestLineLexer : Lexer<RequestLine>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public RequestLineLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<RequestLine> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<RequestLine>.FromResult(new RequestLine(result.Element));
            }
            return ReadResult<RequestLine>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
