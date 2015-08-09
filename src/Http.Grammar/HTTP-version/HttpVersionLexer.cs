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

        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out HttpVersion element)
        {
            Sequence result;
            if (this.innerLexer.TryRead(scanner, null, out result))
            {
                element = new HttpVersion(result);
                if (previousElementOrNull != null)
                {
                    previousElementOrNull.NextElement = element;
                    element.PreviousElement = previousElementOrNull;
                }

                return true;
            }

            element = default(HttpVersion);
            return false;
        }
    }
}