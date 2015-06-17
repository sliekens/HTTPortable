namespace Uri.Grammar
{
    using System;
    using System.Diagnostics;

    using TextFx.ABNF;

    public class PercentEncoding : Sequence
    {
        public PercentEncoding(Sequence sequence)
            : base(sequence)
        {
        }

        public static explicit operator char(PercentEncoding instance)
        {
            return instance.ToChar();
        }

        public char ToChar()
        {
            Debug.Assert(this.Value != null, "this.Value != null");
            Debug.Assert(this.Value.Length == 3, "this.Value.Length == 3");
            return (char)Convert.ToInt32(this.Value.Substring(1), 16);
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