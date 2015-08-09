namespace Http.Grammar
{
    using TextFx;

    public class RequestLineLexer : Lexer<RequestLine>
    {
        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out RequestLine element)
        {
            throw new System.NotImplementedException();
        }
    }
}