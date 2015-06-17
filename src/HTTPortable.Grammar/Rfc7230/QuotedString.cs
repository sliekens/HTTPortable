namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;

    using SLANG;
    using SLANG.Core;

    using QuotedCharacter = SLANG.Alternative<QuotedText, QuotedPair>;

    public class QuotedString : Sequence<DoubleQuote, Repetition<QuotedCharacter>, DoubleQuote>
    {
        public QuotedString(DoubleQuote element1, Repetition<QuotedCharacter> element2, DoubleQuote element3, ITextContext context)
            : base(element1, element2, element3, context)
        {
            Contract.Requires(element1 != null);
            Contract.Requires(element2 != null);
            Contract.Requires(element3 != null);
            Contract.Requires(context != null);
        }
    }
}