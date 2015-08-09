namespace Http.Grammar
{
    using System;

    using TextFx;

    public class FieldValueLexer : Lexer<FieldValue>
    {
        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out FieldValue element)
        {
            throw new NotImplementedException();
        }
    }
}