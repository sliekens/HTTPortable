namespace Http.Grammar
{
    using System;

    using TextFx;

    public class ChunkSizeLexer : Lexer<ChunkSize>
    {
        public override ReadResult<ChunkSize> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            throw new NotImplementedException();
        }
    }
}