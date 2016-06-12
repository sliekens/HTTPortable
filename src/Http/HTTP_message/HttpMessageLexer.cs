using System;
using Txt;
using Txt.ABNF;
using Txt.Core;

namespace Http.HTTP_message
{
    public sealed class HttpMessageLexer : Lexer<HttpMessage>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public HttpMessageLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<HttpMessage> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<HttpMessage>.FromResult(new HttpMessage(result.Element));
            }
            return ReadResult<HttpMessage>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
