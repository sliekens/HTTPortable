namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;

    using SLANG;
    using SLANG.Core;

    public class StatusCode : Element
    {
        public StatusCode(Digit digit1, Digit digit2, Digit digit3, ITextContext context)
            : base(string.Concat(digit1.Data, digit2.Data, digit3.Data), context)
        {
            Contract.Requires(digit1 != null);
            Contract.Requires(digit2 != null);
            Contract.Requires(digit3 != null);
            Contract.Requires(context != null);
        }

        public int ToInt()
        {
            return int.Parse(this.Data);
        }
    }
}
