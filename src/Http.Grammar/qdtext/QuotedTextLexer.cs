namespace Http.Grammar
{
    using System;

    using TextFx;

    public class QuotedTextLexer : Lexer<QuotedText>
    {
        public override ReadResult<QuotedText> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            throw new NotImplementedException();
        }
    }
}