namespace Http.Grammar
{
    using System;

    using TextFx;

    public class ChunkLexer : Lexer<Chunk>
    {
        public override ReadResult<Chunk> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            throw new NotImplementedException();
        }
    }
}