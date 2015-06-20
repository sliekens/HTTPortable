namespace Http.Grammar
{
    using TextFx;

    public class RequestLineLexer : Lexer<RequestLine>
    {
        public override bool TryRead(ITextScanner scanner, out RequestLine element)
        {
            throw new System.NotImplementedException();
        }
    }
}