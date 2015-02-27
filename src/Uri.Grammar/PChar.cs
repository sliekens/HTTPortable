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

        public PChar(SubcomponentsDelimiter subcomponentsDelimiter, ITextContext context)
            : base(subcomponentsDelimiter.Data, context)
        {
            Contract.Requires(subcomponentsDelimiter != null);
        }

        public PChar(char data, ITextContext context)
            : base(data, context)
        {
            Contract.Requires(data == ':' || data == '@');
        }
    }
}