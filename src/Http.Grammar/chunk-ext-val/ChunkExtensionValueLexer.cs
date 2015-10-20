namespace Http.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class ChunkExtensionValueLexer : Lexer<ChunkExtensionValue>
    {
        private readonly ILexer<Alternative> innerLexer;

        public ChunkExtensionValueLexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<ChunkExtensionValue> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return
                    ReadResult<ChunkExtensionValue>.FromError(
                        new SyntaxError
                        {
                            Message = "Expected 'chunk-ext-val'.",
                            RuleName = "chunk-ext-val",
                            Context = scanner.GetContext(),
                            InnerError = result.Error
                        });
            }

            var element = new ChunkExtensionValue(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<ChunkExtensionValue>.FromResult(element);
        }
    }
}