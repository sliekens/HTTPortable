namespace Http.Grammar
{
    using System;

    using TextFx;

    public class HttpMessageLexer : Lexer<HttpMessage>
    {
        public override ReadResult<HttpMessage> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            throw new NotImplementedException();
        }
    }
}