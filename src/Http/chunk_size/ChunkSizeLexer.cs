using System;
using Txt;
using Txt.ABNF;
using Txt.Core;

namespace Http.chunk_size
{
    public sealed class ChunkSizeLexer : Lexer<ChunkSize>
    {
        private readonly ILexer<Repetition> innerLexer;

        public ChunkSizeLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<ChunkSize> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<ChunkSize>.FromResult(new ChunkSize(result.Element));
            }
            return ReadResult<ChunkSize>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}