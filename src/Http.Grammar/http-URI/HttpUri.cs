namespace Http.Grammar
{
    using TextFx.ABNF;

    public class HttpUri : Sequence
    {
        public HttpUri(Sequence sequence)
            : base(sequence)
        {
        }
    }
}