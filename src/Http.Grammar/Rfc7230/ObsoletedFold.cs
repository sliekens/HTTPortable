using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class ObsoletedFold : Element
    {
        public ObsoletedFold(EndOfLine endOfLine, RequiredWhiteSpace requiredWhiteSpace, ITextContext context)
            : base(string.Concat(endOfLine.Data, requiredWhiteSpace.Data), context)
        {
            Contract.Requires(endOfLine != null);
            Contract.Requires(requiredWhiteSpace != null);
        }
    }
}
