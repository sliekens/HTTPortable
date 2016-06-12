using System;
using Txt;
using Txt.ABNF;
using Txt.Core;

namespace Http.chunk_data
{
    public sealed class ChunkDataLexer : Lexer<ChunkData>
    {
        private readonly ILexer<Repetition> innerLexer;

        public ChunkDataLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<ChunkData> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<ChunkData>.FromResult(new ChunkData(result.Element));
            }
            return ReadResult<ChunkData>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}