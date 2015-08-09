namespace Http.Grammar
{
    using System;

    using TextFx;

    public class ChunkSizeLexer : Lexer<ChunkSize>
    {
        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out ChunkSize element)
        {
            throw new NotImplementedException();
        }
    }
}