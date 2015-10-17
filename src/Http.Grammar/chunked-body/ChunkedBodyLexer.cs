namespace Http.Grammar
{
    using System;

    using TextFx;

    public class ChunkedBodyLexer : Lexer<ChunkedBody>
    {
        public override ReadResult<ChunkedBody> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            throw new NotImplementedException();
        }
    }
}