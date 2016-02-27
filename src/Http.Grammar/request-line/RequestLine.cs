namespace Http.Grammar
{
    using TextFx.ABNF;

    public class RequestLine : Concatenation
    {
        public RequestLine(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}
