namespace Http.Grammar
{
    using System;

    using TextFx;

    public class HttpNameLexer : Lexer<HttpName>
    {
        private readonly ILexer<Terminal> innerLexer;

        /// <summary>
        /// </summary>
        /// <param name="innerLexer">%x48.54.54.50</param>
        public HttpNameLexer(ILexer<Terminal> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<HttpName> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<HttpName>.FromError(new SyntaxError
                {
                    Message = "Expected 'HTTP-name'.",
                    RuleName = "HTTP-name",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new HttpName(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<HttpName>.FromResult(element);
        }
    }
}