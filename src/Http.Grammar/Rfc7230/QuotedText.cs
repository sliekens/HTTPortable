namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;

    using SLANG;
    using SLANG.Core;



    public class QuotedText : Element
    {
        private QuotedText(char data, ITextContext context)
            : base(data, context)
        {
        }

        private QuotedText(string data, ITextContext context)
            : base(data, context)
        {
        }

        public static QuotedText Create1(HorizontalTab horizontalTab, ITextContext context)
        {
            Contract.Requires(horizontalTab != null);
            Contract.Requires(context != null);
            return new QuotedText(horizontalTab.Data, context);
        }

        public static QuotedText Create2(Space space, ITextContext context)
        {
            Contract.Requires(space != null);
            Contract.Requires(context != null);
            return new QuotedText(space.Data, context);
        }

        public static QuotedText Create3(char terminal, ITextContext context)
        {
            Contract.Requires(terminal == '!');
            Contract.Requires(context != null);
            return new QuotedText(terminal, context);
        }

        public static QuotedText Create4(char terminal, ITextContext context)
        {
            Contract.Requires(terminal >= '\x0023' && terminal <= '\x005B');
            Contract.Requires(context != null);
            return new QuotedText(terminal, context);
        }

        public static QuotedText Create5(char terminal, ITextContext context)
        {
            Contract.Requires(terminal >= '\x005D' && terminal <= '\x007E');
            Contract.Requires(context != null);
            return new QuotedText(terminal, context);
        }
    }
}