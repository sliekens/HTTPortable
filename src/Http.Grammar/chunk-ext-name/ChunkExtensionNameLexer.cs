namespace Http.Grammar
{
    using System;

    using TextFx;

    public class ChunkExtensionNameLexer : Lexer<ChunkExtensionName>
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

        public override ReadResult<ChunkExtensionName> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return
                    ReadResult<ChunkExtensionName>.FromError(
                        new SyntaxError
                        {
                            Message = "Expected 'chunk-ext-name'.",
                            RuleName = "chunk-ext-name",
                            Context = scanner.GetContext(),
                            InnerError = result.Error
                        });
            }

            var element = new ChunkExtensionName(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<ChunkExtensionName>.FromResult(element);
        }
    }
}