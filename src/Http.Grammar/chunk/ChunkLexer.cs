namespace Http.Grammar
{
    using System;
    using TextFx;
    using TextFx.ABNF;

    public sealed class ChunkLexer : Lexer<Chunk>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public ChunkLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<Chunk> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<Chunk>.FromResult(new Chunk(result.Element));
            }
            return ReadResult<Chunk>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
