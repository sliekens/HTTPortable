namespace Http.Grammar
{
    using TextFx;

    public class TrailerLexer : Lexer<Trailer>
    {
        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out Trailer element)
        {
            throw new System.NotImplementedException();
        }
    }
}