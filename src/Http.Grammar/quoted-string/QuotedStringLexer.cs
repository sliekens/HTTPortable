namespace Http.Grammar
{
    using System;

    using TextFx;

    public class QuotedStringLexer : Lexer<QuotedString>
    {
        public override bool TryRead(ITextScanner scanner, out QuotedString element)
        {
            throw new NotImplementedException();
        }
    }
}