namespace Uri.Grammar
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using SLANG;
    using SLANG.Core;
    using Colon = SLANG.Element;
    using Full_Stop = SLANG.Element;
    using Letter_V = SLANG.Element;

    public class IPvFuture : Letter_V
    {
        public IPvFuture(Element v, IList<HexadecimalDigit> hexadecimalDigits, Element fullStop, 
            IList<Alternative<Unreserved, SubcomponentsDelimiter, Element>> elements, ITextContext context)
            : base(string.Concat(v, string.Concat(hexadecimalDigits), fullStop, string.Concat(elements)), context)
        {
            Contract.Requires(v != null);
            Contract.Requires(hexadecimalDigits != null);
            Contract.Requires(elements != null);
            Contract.Requires(context != null);
        }
    }
}