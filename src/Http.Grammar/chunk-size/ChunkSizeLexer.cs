namespace Http.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class ChunkSizeLexer : Lexer<ChunkSize>
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

        public override ReadResult<ChunkSize> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return
                    ReadResult<ChunkSize>.FromError(
                        new SyntaxError
                        {
                            Message = "Expected 'chunk-size'.",
                            RuleName = "chunk-size",
                            Context = scanner.GetContext(),
                            InnerError = result.Error
                        });
            }

            var element = new ChunkSize(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<ChunkSize>.FromResult(element);
        }
    }
}