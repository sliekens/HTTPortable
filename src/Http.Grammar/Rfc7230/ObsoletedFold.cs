namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;
    using SLANG;
    using SLANG.Core;

    public class ObsoletedFold : Element
    {
        public ObsoletedFold(EndOfLine endOfLine, RequiredWhiteSpace requiredWhiteSpace, ITextContext context)
            : base(string.Concat(endOfLine.Data, requiredWhiteSpace.Data), context)
        {
            Contract.Requires(endOfLine != null);
            Contract.Requires(requiredWhiteSpace != null);
            Contract.Requires(context != null);
        }
    }
}