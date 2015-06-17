namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class IPv4Address : Sequence
    {
        public IPv4Address(Sequence sequence)
            : base(sequence)
        {
        }
    }
}