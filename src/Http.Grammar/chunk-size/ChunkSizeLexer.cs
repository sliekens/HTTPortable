namespace Http.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

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

        public override ReadResult<ChunkSize> Read(ITextScanner scanner)
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