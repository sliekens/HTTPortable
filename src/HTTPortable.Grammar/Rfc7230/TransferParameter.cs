namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;

    using SLANG;

    using BadWhiteSpace = OptionalWhiteSpace;

    public class TransferParameter : Sequence<Token, BadWhiteSpace, Element, BadWhiteSpace, Alternative<Token, QuotedString>>
    {
        public TransferParameter(Token element1, OptionalWhiteSpace element2, Element element3, OptionalWhiteSpace element4, Alternative<Token, QuotedString> element5, ITextContext context)
            : base(element1, element2, element3, element4, element5, context)
        {
            Contract.Requires(element1 != null);
            Contract.Requires(element2 != null);
            Contract.Requires(element3 != null);
            Contract.Requires(element4 != null);
            Contract.Requires(element5 != null);
            Contract.Requires(context != null);
        }
    }
}