namespace Http.Grammar
{
    using System;

    using TextFx;

    public class BadWhiteSpaceLexer : Lexer<BadWhiteSpace>
    {
        private readonly ILexer<OptionalWhiteSpace> innerLexer;

        public BadWhiteSpaceLexer(ILexer<OptionalWhiteSpace> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<BadWhiteSpace> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<BadWhiteSpace>.FromError(new SyntaxError
                {
                    Message = "Expected 'BWS'.",
                    RuleName = "BWS",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new BadWhiteSpace(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<BadWhiteSpace>.FromResult(element);
        }
    }
}