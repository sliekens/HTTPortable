namespace Http.Grammar
{
    using System;

    using TextFx;

    public class StatusLineLexer : Lexer<StatusLine>
    {
        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out StatusLine element)
        {
            throw new NotImplementedException();
        }
    }
}