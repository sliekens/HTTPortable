namespace Http.Grammar
{
    using System;

    using TextFx;

    public class QuotedStringLexer : Lexer<QuotedString>
    {
        public override ReadResult<QuotedString> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            throw new NotImplementedException();
        }
    }
}