namespace Http.Grammar
{
    using System;

    using TextFx;

    public class PseudonymLexer : Lexer<Pseudonym>
    {
        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out Pseudonym element)
        {
            throw new NotImplementedException();
        }
    }
}