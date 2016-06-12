using System;
using Txt;
using Txt.ABNF;
using Txt.Core;

namespace Http.https_URI
{
    public sealed class HttpsUriLexer : Lexer<HttpsUri>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public HttpsUriLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<HttpsUri> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<HttpsUri>.FromResult(new HttpsUri(result.Element));
            }
            return ReadResult<HttpsUri>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
