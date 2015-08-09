namespace Http.Grammar
{
    using System;

    using TextFx;

    public class ChunkExtensionLexer : Lexer<ChunkExtension>
    {
        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out ChunkExtension element)
        {
            throw new NotImplementedException();
        }
    }
}