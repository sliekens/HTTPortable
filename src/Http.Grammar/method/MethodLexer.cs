namespace Http.Grammar
{
    using System;

    using TextFx;

    public class MethodLexer : Lexer<Method>
    {
        private readonly ILexer<Token> innerLexer;

        public MethodLexer(ILexer<Token> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<Method> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return
                    ReadResult<Method>.FromError(
                        new SyntaxError
                        {
                            Message = "Expected 'method'.",
                            RuleName = "method",
                            Context = scanner.GetContext(),
                            InnerError = result.Error
                        });
            }

            var element = new Method(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<Method>.FromResult(element);
        }
    }
}