using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class FieldContent : Element
    {
        public FieldContent(FieldVChar fieldVChar, ITextContext context)
            : base(fieldVChar.Data, context)
        {
            Contract.Requires(fieldVChar != null);
        }

        public FieldContent(FieldVChar fieldVCharLeft, RWS rws, FieldVChar fieldVCharRight, ITextContext context)
            : base(string.Concat(fieldVCharLeft.Data, rws.Data, fieldVCharRight.Data), context)
        {
            Contract.Requires(fieldVCharLeft != null);
            Contract.Requires(rws != null);
            Contract.Requires(fieldVCharRight != null);
        }
    }
}
