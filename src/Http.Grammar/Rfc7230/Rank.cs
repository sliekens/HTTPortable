namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;
    using SLANG;
    using LessThanMaximum = SLANG.Sequence<SLANG.Element, SLANG.Option<SLANG.Sequence<SLANG.Element, SLANG.Repetition<SLANG.Core.Digit>>>>;
    using Maximum = SLANG.Sequence<SLANG.Element, SLANG.Option<SLANG.Sequence<SLANG.Element, SLANG.Repetition<SLANG.Element>>>>;

    public class Rank : Alternative<LessThanMaximum, Maximum>
    {
        public Rank(LessThanMaximum element, ITextContext context)
            : base(element, context)
        {
            Contract.Requires(element != null);
            Contract.Requires(context != null);
        }

        public Rank(Maximum element, ITextContext context)
            : base(element, context)
        {
            Contract.Requires(element != null);
            Contract.Requires(context != null);
        }
    }
}