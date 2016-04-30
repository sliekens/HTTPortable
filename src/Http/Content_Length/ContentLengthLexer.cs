using System;
using Txt;
using Txt.ABNF;

namespace Http.Content_Length
{
    public sealed class ContentLengthLexer : Lexer<ContentLength>
    {
        private readonly ILexer<Repetition> innerLexer;

        public ContentLengthLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<ContentLength> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<ContentLength>.FromResult(new ContentLength(result.Element));
            }
            return ReadResult<ContentLength>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
