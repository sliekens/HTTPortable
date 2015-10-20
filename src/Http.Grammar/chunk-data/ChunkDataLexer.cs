namespace Http.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class ChunkDataLexer : Lexer<ChunkData>
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

        public override ReadResult<ChunkData> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<ChunkData>.FromError(new SyntaxError
                {
                    Message = "Expected 'chunk-data'.",
                    RuleName = "chunk-data",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new ChunkData(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<ChunkData>.FromResult(element);
        }
    }
}