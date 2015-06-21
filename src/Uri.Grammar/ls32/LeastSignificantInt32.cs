namespace Uri.Grammar
{
    using System.Linq;

    using TextFx.ABNF;

    public class LeastSignificantInt32 : Alternative
    {
        public LeastSignificantInt32(Alternative alternative)
            : base(alternative)
        {
        }

        public byte[] GetBytes()
        {
            var thisAsIPv4 = this.Element as IPv4Address;
            if (thisAsIPv4 != null)
            {
                return thisAsIPv4.GetBytes();
            }

            var seq = (Sequence)this.Element;
            return seq.Elements.OfType<HexadecimalInt16>().SelectMany(int16 => int16.GetBytes()).ToArray();
        }
    }
}