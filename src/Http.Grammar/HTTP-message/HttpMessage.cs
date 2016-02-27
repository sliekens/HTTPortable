namespace Http.Grammar
{
    using TextFx.ABNF;

    public class HttpMessage : Concatenation
    {
        public HttpMessage(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}