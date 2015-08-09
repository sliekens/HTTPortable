namespace Http.Grammar
{
    using System;

    using TextFx;

    public class QuotedTextLexer : Lexer<QuotedText>
    {
        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out QuotedText element)
        {
            throw new NotImplementedException();
        }
    }
}