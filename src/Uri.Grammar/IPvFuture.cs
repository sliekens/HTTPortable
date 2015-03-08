namespace Uri.Grammar
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    using SLANG;
    using SLANG.Core;



    using Letter_V = SLANG.Element;
    using Full_Stop = SLANG.Element;
    using Colon = SLANG.Element;
    public class IPvFuture : Letter_V
    {
        public IPvFuture(Letter_V v, IList<HexadecimalDigit> hexadecimalDigits, Full_Stop fullStop, IList<Alternative<Unreserved, SubcomponentsDelimiter, Colon>> elements, ITextContext context)
            : base(string.Concat(v, string.Concat(hexadecimalDigits), fullStop, string.Concat(elements)), context)
        {
            Contract.Requires(v != null);
            Contract.Requires(hexadecimalDigits != null);
            Contract.Requires(elements != null);
            Contract.Requires(context != null);
        }
    }
}
