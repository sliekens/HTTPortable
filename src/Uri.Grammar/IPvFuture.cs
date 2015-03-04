namespace Uri.Grammar
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    using Text.Scanning;
    using Text.Scanning.Core;

    using Letter_V = Text.Scanning.Element;
    using Full_Stop = Text.Scanning.Element;
    using Colon = Text.Scanning.Element;
    public class IPvFuture : Element
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
