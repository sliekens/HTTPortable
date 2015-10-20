namespace Http.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class ChunkExtensionLexer : Lexer<ChunkExtension>
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

        public override ReadResult<ChunkExtension> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return
                    ReadResult<ChunkExtension>.FromError(
                        new SyntaxError
                        {
                            Message = "Expected 'chunk-ext'.",
                            RuleName = "chunk-ext",
                            Context = scanner.GetContext(),
                            InnerError = result.Error
                        });
            }

            var element = new ChunkExtension(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<ChunkExtension>.FromResult(element);
        }
    }
}