namespace Http.Grammar
{
    using TextFx.ABNF;

    public class LastChunk : Sequence
    {
        public LastChunk(Sequence sequence)
            : base(sequence)
        {
        }
    }
}