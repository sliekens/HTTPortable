namespace Http.Grammar
{
    using System;

    using TextFx;

    public class HttpUriLexer : Lexer<HttpUri>
    {
        public override bool TryRead(ITextScanner scanner, out HttpUri element)
        {
            throw new NotImplementedException();
        }
    }
}