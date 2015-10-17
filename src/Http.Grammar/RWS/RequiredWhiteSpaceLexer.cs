namespace Http.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class RequiredWhiteSpaceLexer : Lexer<RequiredWhiteSpace>
    {
        private readonly ILexer<Repetition> innerLexer;

        /// <summary>
        /// </summary>
        /// <param name="innerLexer">1*( SP / HTAB )</param>
        public RequiredWhiteSpaceLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<RequiredWhiteSpace> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<RequiredWhiteSpace>.FromError(new SyntaxError
                {
                    Message = "Expected 'RWS'.",
                    RuleName = "RWS",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new RequiredWhiteSpace(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<RequiredWhiteSpace>.FromResult(element);
        }
    }
}