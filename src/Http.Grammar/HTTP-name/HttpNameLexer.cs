namespace Http.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class HttpNameLexer : Lexer<HttpName>
    {
        private readonly ILexer<TerminalString> innerLexer;

        /// <summary>
        /// </summary>
        /// <param name="innerLexer">%x48.54.54.50</param>
        public HttpNameLexer(ILexer<TerminalString> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, out HttpName element)
        {
            TerminalString result;
            if (this.innerLexer.TryRead(scanner, out result))
            {
                element = new HttpName(result);
                return true;
            }

            element = default(HttpName);
            return false;
        }
    }
}