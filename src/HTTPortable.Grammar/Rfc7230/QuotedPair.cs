namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;

    using SLANG;

    using EscapedCharacter = SLANG.Alternative<SLANG.Core.HorizontalTab, SLANG.Core.Space, SLANG.Core.VisibleCharacter, ObsoletedText>;

    public class QuotedPair : Sequence<Element, EscapedCharacter>
    {
        public QuotedPair(Element element1, EscapedCharacter element2, ITextContext context)
            : base(element1, element2, context)
        {
            Contract.Requires(element1 != null);
            Contract.Requires(element2 != null);
            Contract.Requires(context != null);
        }
    }
}