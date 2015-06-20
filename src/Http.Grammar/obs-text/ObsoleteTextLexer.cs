namespace Http.Grammar
{
    using System;

    using TextFx;

    public class ObsoleteTextLexer : Lexer<ObsoleteText>
    {
        public override bool TryRead(ITextScanner scanner, out ObsoleteText element)
        {
            throw new NotImplementedException();
        }
    }
}