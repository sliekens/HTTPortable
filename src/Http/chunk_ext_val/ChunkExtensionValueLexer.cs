using System;
using Txt;
using Txt.ABNF;
using Txt.Core;

namespace Http.chunk_ext_val
{
    public sealed class ChunkExtensionValueLexer : Lexer<ChunkExtensionValue>
    {
        private readonly ILexer<Alternation> innerLexer;

        public ChunkExtensionValueLexer(ILexer<Alternation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<ChunkExtensionValue> ReadImpl(ITextScanner scanner)
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