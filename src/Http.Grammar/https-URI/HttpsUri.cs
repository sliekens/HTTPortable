namespace Http.Grammar
{
    using TextFx.ABNF;

    public class HttpsUri : Concatenation
    {
        public HttpsUri(Concatenation concatenation)
            : base(concatenation)
        {
        }
    }
}