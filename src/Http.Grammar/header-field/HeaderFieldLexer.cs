namespace Http.Grammar
{
    using System;

    using TextFx;

    public class HeaderFieldLexer : Lexer<HeaderField>
    {
        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out HeaderField element)
        {
            throw new NotImplementedException();
        }
    }
}
