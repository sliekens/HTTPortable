namespace Http.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class OptionalWhiteSpaceLexer : Lexer<OptionalWhiteSpace>
    {
        private readonly ILexer<Repetition> innerLexer;

        /// <summary>
        /// </summary>
        /// <param name="innerLexer">*( SP / HTAB )</param>
        public OptionalWhiteSpaceLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<OptionalWhiteSpace> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<OptionalWhiteSpace>.FromError(new SyntaxError
                {
                    Message = "Expected 'OWS'.",
                    RuleName = "OWS",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new OptionalWhiteSpace(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<OptionalWhiteSpace>.FromResult(element);
        }
    }
}