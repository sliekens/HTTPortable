namespace Http.Grammar
{
    using System;

    using TextFx;

    public class StatusLineLexer : Lexer<StatusLine>
    {
        public override bool TryRead(ITextScanner scanner, out StatusLine element)
        {
            throw new NotImplementedException();
        }
    }
}