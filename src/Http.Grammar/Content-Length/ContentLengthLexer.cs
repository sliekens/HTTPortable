namespace Http.Grammar
{
    using System;

    using TextFx;

    public class ContentLengthLexer : Lexer<ContentLength>
    {
        public override bool TryRead(ITextScanner scanner, out ContentLength element)
        {
            throw new NotImplementedException();
        }
    }
}