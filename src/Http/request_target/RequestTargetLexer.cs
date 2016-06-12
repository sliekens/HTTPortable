using System;
using Txt;
using Txt.ABNF;
using Txt.Core;

namespace Http.request_target
{
    public sealed class RequestTargetLexer : Lexer<RequestTarget>
    {
        private readonly ILexer<Alternation> innerLexer;

        public RequestTargetLexer(ILexer<Alternation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<RequestTarget> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<RequestTarget>.FromResult(new RequestTarget(result.Element));
            }
            return ReadResult<RequestTarget>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
