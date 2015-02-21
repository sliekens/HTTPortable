using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Uri.Grammar
{
    public class PctEncodedToken : Token
    {
        public PctEncodedToken(HexDigToken hexDig1, HexDigToken hexDig2, ITextContext context)
            : base(string.Concat("%", hexDig1.Data, hexDig2.Data), context)
        {
            Contract.Requires(hexDig1 != null);
            Contract.Requires(hexDig2 != null);
        }
    }
}