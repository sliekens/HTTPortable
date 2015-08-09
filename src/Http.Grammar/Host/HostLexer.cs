namespace Http.Grammar
{
    using System;

    using TextFx;

    public class HostLexer : Lexer<Host>
    {
        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out Host element)
        {
            throw new NotImplementedException();
        }
    }
}