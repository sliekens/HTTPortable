using System;
using Txt;
using Txt.ABNF;
using Txt.Core;

namespace Http.chunked_body
{
    public sealed class ChunkedBodyLexer : Lexer<ChunkedBody>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public ChunkedBodyLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<ChunkedBody> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<ChunkedBody>.FromResult(new ChunkedBody(result.Element));
            }
            return ReadResult<ChunkedBody>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}