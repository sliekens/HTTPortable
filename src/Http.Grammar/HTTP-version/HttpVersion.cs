namespace Http.Grammar
{
    using TextFx.ABNF;

    public class HttpVersion : Concatenation
    {
        public HttpVersion(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}