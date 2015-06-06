namespace Uri.Grammar
{
    using System.Diagnostics;

    using SLANG;

    public class PercentEncoding : Sequence
    {
        public PercentEncoding(Sequence sequence)
            : base(sequence)
        {
        }

        public char ToChar()
        {
            Debug.Assert(this.Values != null, "this.Values != null");
            Debug.Assert(this.Values.Length == 3, "this.Values.Length == 3");
            return (char)System.Convert.ToInt32(this.Values.Substring(1), 16);
        }

        public override string GetWellFormedText()
        {
            /* 6.2.2.1.  Case Normalization
             * 
             * For all URIs, the hexadecimal digits within a percent-encoding
             * triplet (e.g., "%3a" versus "%3A") are case-insensitive and therefore
             * should be normalized to use uppercase letters for the digits A-F.
             */
            return base.GetWellFormedText().ToUpperInvariant();
        }
    }
}