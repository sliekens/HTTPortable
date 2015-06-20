namespace Http.Grammar
{
    using System;

    using TextFx;

    public class ChunkSizeLexer : Lexer<ChunkSize>
    {
        public override bool TryRead(ITextScanner scanner, out ChunkSize element)
        {
            throw new NotImplementedException();
        }
    }
}