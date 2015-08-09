namespace Http.Grammar
{
    using System;

    using TextFx;

    public class ChunkDataLexer : Lexer<ChunkData>
    {
        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out ChunkData element)
        {
            throw new NotImplementedException();
        }
    }
}