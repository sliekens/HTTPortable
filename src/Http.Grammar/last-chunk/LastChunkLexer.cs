namespace Http.Grammar
{
    using System;

    using TextFx;

    public class LastChunkLexer : Lexer<LastChunk>
    {
        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out LastChunk element)
        {
            throw new NotImplementedException();
        }
    }
}