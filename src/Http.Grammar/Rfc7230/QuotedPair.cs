namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;

    using Text.Scanning;
    using Text.Scanning.Core;

    public class QuotedPair : Element
    {
        public QuotedPair(Element backslash, HorizontalTab horizontalTab, ITextContext context)
            : base(string.Concat(backslash, horizontalTab), context)
        {
            Contract.Requires(backslash != null && backslash.Data == @"\");
            Contract.Requires(horizontalTab != null);
            Contract.Requires(context != null);
        }

        public QuotedPair(Element backslash, Space space, ITextContext context)
            : base(string.Concat(backslash, space), context)
        {
            Contract.Requires(backslash != null && backslash.Data == @"\");
            Contract.Requires(space != null);
            Contract.Requires(context != null);
        }

        public QuotedPair(Element backslash, VisibleCharacter visibleCharacter, ITextContext context)
            : base(string.Concat(backslash, visibleCharacter), context)
        {
            Contract.Requires(backslash != null && backslash.Data == @"\");
            Contract.Requires(visibleCharacter != null);
            Contract.Requires(context != null);
        }

        public QuotedPair(Element backslash, ObsoletedText obsoletedText, ITextContext context)
            : base(string.Concat(backslash, obsoletedText), context)
        {
            Contract.Requires(backslash != null && backslash.Data == @"\");
            Contract.Requires(obsoletedText != null);
            Contract.Requires(context != null);
        }
    }
}
