namespace Http.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class AbsolutePathLexer : Lexer<AbsolutePath>
    {
        private readonly ILexer<Repetition> innerLexer;

        public AbsolutePathLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<AbsolutePath> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return
                    ReadResult<AbsolutePath>.FromError(
                        new SyntaxError
                        {
                            Message = "Expected 'absolute-path'.",
                            RuleName = "absolute-path",
                            Context = scanner.GetContext(),
                            InnerError = result.Error
                        });
            }

            var element = new AbsolutePath(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<AbsolutePath>.FromResult(element);
        }
    }
}