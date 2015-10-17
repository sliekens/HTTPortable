namespace Http.Grammar
{
    using System;

    using TextFx;

    public class LastChunkLexer : Lexer<LastChunk>
    {
        public override ReadResult<LastChunk> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            throw new NotImplementedException();
        }
    }
}