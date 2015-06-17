namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;

    using SLANG;

    public class ObsoletedText : Element
    {
        public ObsoletedText(char data, ITextContext context)
            : base(data, context)
        {
            Contract.Requires(data >= '\x80' && data <= '\xFF');
            Contract.Requires(context != null);
        }
    }
}