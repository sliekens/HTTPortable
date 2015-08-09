namespace Http.Grammar
{
    using System;

    using TextFx;

    public class FieldNameLexer : Lexer<FieldName>
    {
        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out FieldName element)
        {
            throw new NotImplementedException();
        }
    }
}