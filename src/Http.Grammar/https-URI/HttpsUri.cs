namespace Http.Grammar
{
    using TextFx.ABNF;

    public class HttpsUri : Sequence
    {
        public HttpsUri(Sequence sequence)
            : base(sequence)
        {
        }
    }
}