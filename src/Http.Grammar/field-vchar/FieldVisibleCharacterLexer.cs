namespace Http.Grammar
{
    using System;

    using TextFx;

    public class FieldVisibleCharacterLexer : Lexer<FieldVisibleCharacter>
    {
        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out FieldVisibleCharacter element)
        {
            throw new NotImplementedException();
        }
    }
}