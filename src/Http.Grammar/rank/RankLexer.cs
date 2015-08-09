namespace Http.Grammar
{
    using System;

    using TextFx;

    public class RankLexer : Lexer<Rank>
    {
        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out Rank element)
        {
            throw new NotImplementedException();
        }
    }
}