using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Uri.Grammar
{
    public class ReservedToken : Element
    {
        public ReservedToken(Alternative<GenDelimsToken, SubDelimsToken> data, ITextContext context)
            : base(data.Element.Data, context)
        {
            Contract.Requires(data != null);
            Contract.Requires(data.Element != null);
        }
    }
}