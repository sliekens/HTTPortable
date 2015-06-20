namespace Http.Grammar
{
    using TextFx.ABNF;

    public class HttpMessage : Sequence
    {
        public HttpMessage(Sequence sequence)
            : base(sequence)
        {
        }
    }
}