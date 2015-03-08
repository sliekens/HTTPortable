using System.Diagnostics.Contracts;

using SLANG.Core;

namespace Uri.Grammar
{
    using SLANG;
    using SLANG.Core;

    public class PercentEncoding : Element
    {
        public PercentEncoding(HexadecimalDigit hexadecimalDigit1, HexadecimalDigit hexadecimalDigit2, ITextContext context)
            : base(string.Concat("%", hexadecimalDigit1.Data, hexadecimalDigit2.Data), context)
        {
            Contract.Requires(hexadecimalDigit1 != null);
            Contract.Requires(hexadecimalDigit2 != null);
        }
    }
}