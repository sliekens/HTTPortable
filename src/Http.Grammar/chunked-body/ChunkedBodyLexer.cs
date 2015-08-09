namespace Http.Grammar
{
    using System;

    using TextFx;

    public class ChunkedBodyLexer : Lexer<ChunkedBody>
    {
        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out ChunkedBody element)
        {
            throw new NotImplementedException();
        }
    }
}