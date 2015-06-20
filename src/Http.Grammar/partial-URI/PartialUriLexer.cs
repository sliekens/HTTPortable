namespace Http.Grammar
{
    using System;

    using TextFx;

    public class PartialUriLexer : Lexer<PartialUri>
    {
        public override bool TryRead(ITextScanner scanner, out PartialUri element)
        {
            throw new NotImplementedException();
        }
    }
}