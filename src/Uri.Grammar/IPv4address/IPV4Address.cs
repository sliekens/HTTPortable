namespace Uri.Grammar
{
    using System.Linq;

    using TextFx.ABNF;

    public class IPv4Address : Concatenation
    {
        public IPv4Address(Concatenation concatenation)
            : base(concatenation)
        {
        }

        public byte[] GetBytes()
        {
            var octets = Elements.OfType<DecimalOctet>();
            return octets.Select(octet => octet.ToByte()).ToArray();
        }
    }
}