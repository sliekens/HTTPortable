namespace Http.Grammar
{
    using System;

    using TextFx;

    public class StartLineLexer : Lexer<StartLine>
    {
        public override bool TryRead(ITextScanner scanner, out StartLine element)
        {
            throw new NotImplementedException();
        }
    }
}