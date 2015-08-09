namespace Http.Grammar
{
    using System;

    using TextFx;

    public class ObsoleteFoldLexer : Lexer<ObsoleteFold>
    {
        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out ObsoleteFold element)
        {
            throw new NotImplementedException();
        }
    }
}