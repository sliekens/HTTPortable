namespace Http.Grammar
{
    using System;

    using TextFx;

    public class OriginFormLexer : Lexer<OriginForm>
    {
        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out OriginForm element)
        {
            throw new NotImplementedException();
        }
    }
}