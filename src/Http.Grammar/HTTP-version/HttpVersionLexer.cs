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

        public override bool TryRead(ITextScanner scanner, out HttpVersion element)
        {
            Sequence result;
            if (this.innerLexer.TryRead(scanner, out result))
            {
                element = new HttpVersion(result);
                return true;
            }

            element = default(HttpVersion);
            return false;
        }
    }
}