namespace Http.Grammar
{
    using System;

    using TextFx;

    public class AbsolutePathLexer : Lexer<AbsolutePath>
    {
        public override bool TryRead(ITextScanner scanner, out AbsolutePath element)
        {
            throw new NotImplementedException();
        }
    }
}