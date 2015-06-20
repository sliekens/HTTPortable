namespace Http.Grammar
{
    using TextFx.ABNF;

    public class HttpVersion : Sequence
    {
        public HttpVersion(Sequence sequence)
            : base(sequence)
        {
        }
    }
}