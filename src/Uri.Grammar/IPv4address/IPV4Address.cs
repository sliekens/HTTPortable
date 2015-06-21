namespace Uri.Grammar
{
    using System.Linq;

    using TextFx.ABNF;

    public class IPv4Address : Sequence
    {
        public IPv4Address(Sequence sequence)
            : base(sequence)
        {
        }

        public byte[] GetBytes()
        {
            var octets = this.Elements.OfType<DecimalOctet>();
            return octets.Select(octet => octet.ToByte()).ToArray();
        }
    }
}