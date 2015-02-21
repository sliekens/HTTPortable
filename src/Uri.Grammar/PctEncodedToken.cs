using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Uri.Grammar
{
    public class PctEncodedToken : Element
    {
        public PctEncodedToken(HexadecimalDigit hexadecimalDigit1, HexadecimalDigit hexadecimalDigit2, ITextContext context)
            : base(string.Concat("%", hexadecimalDigit1.Data, hexadecimalDigit2.Data), context)
        {
            Contract.Requires(hexadecimalDigit1 != null);
            Contract.Requires(hexadecimalDigit2 != null);
        }
    }
}