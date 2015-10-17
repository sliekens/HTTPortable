namespace Http.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class HttpVersionLexer : Lexer<HttpVersion>
    {
        private readonly ILexer<Sequence> innerLexer;

        /// <summary>
        /// </summary>
        /// <param name="innerLexer">HTTP-name "/" DIGIT "." DIGIT</param>
        public HttpVersionLexer(ILexer<Sequence> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<HttpVersion> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<HttpVersion>.FromError(new SyntaxError
                {
                    Message = "Expected 'HTTP-version'.",
                    RuleName = "HTTP-version",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new HttpVersion(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<HttpVersion>.FromResult(element);
        }
    }
}