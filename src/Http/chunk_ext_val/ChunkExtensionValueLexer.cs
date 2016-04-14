using System;
using Txt;
using Txt.ABNF;

namespace Http.chunk_ext_val
{
    public sealed class ChunkExtensionValueLexer : Lexer<ChunkExtensionValue>
    {
        private readonly ILexer<Alternative> innerLexer;

        public ChunkExtensionValueLexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<ChunkExtensionValue> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<ChunkExtensionValue>.FromResult(new ChunkExtensionValue(result.Element));
            }
            return ReadResult<ChunkExtensionValue>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}