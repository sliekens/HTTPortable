namespace Http.Grammar
{
    using TextFx.ABNF;

    public class Chunk : Sequence
    {
        public Chunk(Sequence sequence)
            : base(sequence)
        {
        }
    }
}