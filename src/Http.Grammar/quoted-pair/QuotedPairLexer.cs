namespace Http.Grammar
{
    using System;

    using TextFx;

    public class QuotedPairLexer : Lexer<QuotedPair>
    {
        public override bool TryRead(ITextScanner scanner, out QuotedPair element)
        {
            throw new NotImplementedException();
        }
    }
}