namespace Http.Grammar
{
    using System;

    using TextFx;

    public sealed class ChunkExtensionNameLexer : Lexer<ChunkExtensionName>
    {
        private readonly ILexer<Token> innerLexer;

        public ChunkExtensionNameLexer(ILexer<Token> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<ChunkExtensionName> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<ChunkExtensionName>.FromResult(new ChunkExtensionName(result.Element));
            }
            return ReadResult<ChunkExtensionName>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}