namespace Http.Grammar
{
    using System;

    using TextFx;

    public class AbsoluteFormLexer : Lexer<AbsoluteForm>
    {
        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out AbsoluteForm element)
        {
            throw new NotImplementedException();
        }
    }
}