namespace Http.Grammar
{
    using TextFx.ABNF;

    public class Via : Sequence
    {
        public Via(Sequence sequence)
            : base(sequence)
        {
        }
    }
}