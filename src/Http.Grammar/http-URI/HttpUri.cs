namespace Http.Grammar
{
    using TextFx.ABNF;

    public class HttpUri : Concatenation
    {
        public HttpUri(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}