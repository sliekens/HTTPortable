namespace Uri.Grammar
{
    using TextFx.ABNF;

    public class DecimalOctet : Alternative
    {
        public DecimalOctet(Alternative alternative)
            : base(alternative)
        {
        }

        public byte ToByte()
        {
            return byte.Parse(this.Value);
        }
    }
}