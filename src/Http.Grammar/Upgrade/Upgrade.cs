namespace Http.Grammar
{
    using TextFx.ABNF;

    public class Upgrade : Sequence
    {
        public Upgrade(Sequence sequence)
            : base(sequence)
        {
        }
    }
}