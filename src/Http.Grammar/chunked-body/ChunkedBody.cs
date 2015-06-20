namespace Http.Grammar
{
    using TextFx.ABNF;

    public class ChunkedBody : Sequence
    {
        public ChunkedBody(Sequence sequence)
            : base(sequence)
        {
        }
    }
}