namespace Uri.Grammar
{
    using System.Diagnostics.Contracts;

    using Text.Scanning;
    using Text.Scanning.Core;

    public class HexadecimalInt16 : Element
    {
        public HexadecimalInt16(HexadecimalDigit digit, ITextContext context)
            : base(digit.Data, context)
        {
            Contract.Requires(digit != null);
        }

        public HexadecimalInt16(HexadecimalDigit digit1, HexadecimalDigit digit2, ITextContext context)
            : base(string.Concat(digit1.Data, digit2.Data), context)
        {
            Contract.Requires(digit1 != null);
            Contract.Requires(digit2 != null);
        }

        public HexadecimalInt16(HexadecimalDigit digit1, HexadecimalDigit digit2, HexadecimalDigit digit3, ITextContext context)
            : base(string.Concat(digit1.Data, digit2.Data, digit3.Data), context)
        {
            Contract.Requires(digit1 != null);
            Contract.Requires(digit2 != null);
            Contract.Requires(digit3 != null);
        }

        public HexadecimalInt16(HexadecimalDigit digit1, HexadecimalDigit digit2, HexadecimalDigit digit3, HexadecimalDigit digit4, ITextContext context)
            : base(string.Concat(digit1.Data, digit2.Data, digit3.Data, digit4.Data), context)
        {
            Contract.Requires(digit1 != null);
            Contract.Requires(digit2 != null);
            Contract.Requires(digit3 != null);
            Contract.Requires(digit4 != null);
        }
    }
}
