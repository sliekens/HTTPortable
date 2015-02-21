using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Uri.Grammar
{
    public class PCharToken : Token
    {
        public PCharToken(UnreservedToken unreserved, ITextContext context)
            : base(unreserved.Data, context)
        {
            Contract.Requires(unreserved != null);
        }

        public PCharToken(PctEncodedToken pctEncoded, ITextContext context)
            : base(pctEncoded.Data, context)
        {
            Contract.Requires(pctEncoded != null);
        }

        public PCharToken(SubDelimsToken subDelims, ITextContext context)
            : base(subDelims.Data, context)
        {
            Contract.Requires(subDelims != null);
        }

        public PCharToken(char data, ITextContext context)
            : base(data, context)
        {
            Contract.Requires(data == ':' || data == '@');
        }
    }
}