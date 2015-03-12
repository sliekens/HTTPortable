namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;
    using SLANG;
    using SLANG.Core;

    public class StatusCode : Sequence<Digit, Digit, Digit>
    {
        public StatusCode(Digit element1, Digit element2, Digit element3, ITextContext context)
            : base(element1, element2, element3, context)
        {
            Contract.Requires(element1 != null);
            Contract.Requires(element2 != null);
            Contract.Requires(element3 != null);
            Contract.Requires(context != null);
        }

        public int ToInt()
        {
            return int.Parse(this.Data);
        }
    }
}