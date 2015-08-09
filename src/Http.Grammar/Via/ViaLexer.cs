namespace Http.Grammar
{
    using TextFx;

    public class ViaLexer : Lexer<Via>
    {
        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out Via element)
        {
            throw new System.NotImplementedException();
        }
    }
}