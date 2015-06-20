namespace Http.Grammar
{
    using System;

    using TextFx;

    public class LastChunkLexer : Lexer<LastChunk>
    {
        public override bool TryRead(ITextScanner scanner, out LastChunk element)
        {
            throw new NotImplementedException();
        }
    }
}