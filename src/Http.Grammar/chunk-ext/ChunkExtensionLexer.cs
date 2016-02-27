namespace Http.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public sealed class ChunkExtensionLexer : Lexer<ChunkExtension>
    {
        private readonly ILexer<Repetition> innerLexer;

        public ChunkExtensionLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<ChunkExtension> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<ChunkExtension>.FromResult(new ChunkExtension(result.Element));
            }
            return ReadResult<ChunkExtension>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}