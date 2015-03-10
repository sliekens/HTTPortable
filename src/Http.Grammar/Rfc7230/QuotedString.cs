namespace Http.Grammar.Rfc7230
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using SLANG;
    using SLANG.Core;

    public class QuotedString : Element
    {
        private QuotedString(char data, ITextContext context)
            : base(data, context)
        {
        }

        private QuotedString(string data, ITextContext context)
            : base(data, context)
        {
        }

        public static QuotedString Create(DoubleQuote openingDoubleQuote, 
            IList<Alternative<QuotedText, QuotedPair>> text, DoubleQuote closingDoubleQuote, ITextContext context)
        {
            Contract.Requires(openingDoubleQuote != null);
            Contract.Requires(text != null);
            Contract.Requires(Contract.ForAll(text, alternative => alternative != null));
            Contract.Requires(closingDoubleQuote != null);
            Contract.Requires(context != null);
            return new QuotedString(string.Concat(openingDoubleQuote, string.Concat(text), closingDoubleQuote), context);
        }
    }
}