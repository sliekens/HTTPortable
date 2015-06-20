namespace Http.Grammar
{
    using TextFx.ABNF;

    public class Trailer : Sequence
    {
        public Trailer(Sequence sequence)
            : base(sequence)
        {
        }
    }
}
