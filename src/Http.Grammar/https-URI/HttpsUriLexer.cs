namespace Http.Grammar
{
    using System;

    using TextFx;

    public class HttpsUriLexer : Lexer<HttpsUri>
    {
        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out HttpsUri element)
        {
            throw new NotImplementedException();
        }
    }
}