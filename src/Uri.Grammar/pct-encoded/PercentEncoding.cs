namespace Uri.Grammar
{
    using SLANG;

    public class PercentEncoding : Sequence
    {
        public PercentEncoding(Sequence sequence)
            : base(sequence)
        {
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