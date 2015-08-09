namespace Http.Grammar
{
    using System;

    using TextFx;

    public class MethodLexer : Lexer<Method>
    {
        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out Method element)
        {
            throw new NotImplementedException();
        }
    }
}