namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;

    using SLANG;
    using SLANG.Core;

    public class ObsoletedFold : Sequence<EndOfLine, RequiredWhiteSpace>
    {
        public ObsoletedFold(EndOfLine element1, RequiredWhiteSpace element2, ITextContext context)
            : base(element1, element2, context)
        {
            Contract.Requires(element1 != null);
            Contract.Requires(element2 != null);
            Contract.Requires(context != null);
        }
    }
}