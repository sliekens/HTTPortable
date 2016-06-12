using System;
using Txt;
using Txt.ABNF;
using Txt.Core;

namespace Http.http_URI
{
    public sealed class HttpUriLexer : Lexer<HttpUri>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public HttpUriLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<HttpUri> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<HttpUri>.FromResult(new HttpUri(result.Element));
            }
            return ReadResult<HttpUri>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
