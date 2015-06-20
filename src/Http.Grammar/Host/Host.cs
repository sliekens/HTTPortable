namespace Http.Grammar
{
    using TextFx.ABNF;

    public class Host : Sequence
    {
        public Host(Sequence sequence)
            : base(sequence)
        {
        }
    }
}