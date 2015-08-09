namespace Http.Grammar
{
    using System;

    using TextFx;

    public class ChunkExtensionValueLexer : Lexer<ChunkExtensionValue>
    {
        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out ChunkExtensionValue element)
        {
            throw new NotImplementedException();
        }
    }
}