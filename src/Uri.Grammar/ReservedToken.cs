using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Uri.Grammar
{
    public class ReservedToken : Token
    {
        public ReservedToken(TokenMutex<GenDelimsToken, SubDelimsToken> data, ITextContext context)
            : base(data.Token.Data, context)
        {
            Contract.Requires(data != null);
            Contract.Requires(data.Token != null);
        }
    }
}