namespace Http.Grammar
{
    using System;

    using TextFx;

    public class ContentLengthLexer : Lexer<ContentLength>
    {
        public override ReadResult<ContentLength> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            throw new NotImplementedException();
        }
    }
}