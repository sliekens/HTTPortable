namespace Http.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class StatusCodeLexer : Lexer<StatusCode>
    {
        private readonly ILexer<Repetition> innerLexer;

        /// <summary>
        /// </summary>
        /// <param name="innerLexer">3DIGIT</param>
        public StatusCodeLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<StatusCode> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<StatusCode>.FromError(new SyntaxError
                {
                    Message = "Expected 'status-code'.",
                    RuleName = "status-code",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new StatusCode(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<StatusCode>.FromResult(element);
        }
    }
}