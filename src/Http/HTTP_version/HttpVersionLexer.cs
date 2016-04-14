using System;
using Txt;
using Txt.ABNF;

namespace Http.HTTP_version
{
    public sealed class HttpVersionLexer : Lexer<HttpVersion>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public HttpVersionLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<HttpVersion> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<HttpVersion>.FromResult(new HttpVersion(result.Element));
            }
            return ReadResult<HttpVersion>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}