namespace Http.Grammar
{
    using System;

    using TextFx;

    public class ObsoleteTextLexer : Lexer<ObsoleteText>
    {
        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out ObsoleteText element)
        {
            throw new NotImplementedException();
        }
    }
}