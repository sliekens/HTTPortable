using System;
using Txt;
using Txt.ABNF;

namespace Http.last_chunk
{
    public sealed class LastChunkLexer : Lexer<LastChunk>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public LastChunkLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<LastChunk> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<LastChunk>.FromResult(new LastChunk(result.Element));
            }
            return ReadResult<LastChunk>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
