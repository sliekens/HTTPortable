namespace Http.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class ChunkLexer : Lexer<Chunk>
    {
        private ILexer<Sequence> innerLexer;

        public ChunkLexer(ILexer<Sequence> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<Chunk> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<Chunk>.FromError(new SyntaxError
                {
                    Message = "Expected 'chunk'.",
                    RuleName = "chunk",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new Chunk(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<Chunk>.FromResult(element);
        }
    }
}