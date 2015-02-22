using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Uri.Grammar
{
    public class PChar : Element
    {
        public PChar(Unreserved unreserved, ITextContext context)
            : base(unreserved.Data, context)
        {
            Contract.Requires(unreserved != null);
        }

        public PChar(PctEncoded pctEncoded, ITextContext context)
            : base(pctEncoded.Data, context)
        {
            Contract.Requires(pctEncoded != null);
        }

        public PChar(SubDelims subDelims, ITextContext context)
            : base(subDelims.Data, context)
        {
            Contract.Requires(subDelims != null);
        }

        public PChar(char data, ITextContext context)
            : base(data, context)
        {
            Contract.Requires(data == ':' || data == '@');
        }
    }
}