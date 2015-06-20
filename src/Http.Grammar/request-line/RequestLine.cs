namespace Http.Grammar
{
    using TextFx.ABNF;

    public class RequestLine : Sequence
    {
        public RequestLine(Sequence sequence)
            : base(sequence)
        {
        }
    }
}
