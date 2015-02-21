using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class FieldContentToken : Element
    {
        public FieldContentToken(FieldVCharToken fieldVChar, ITextContext context)
            : base(fieldVChar.Data, context)
        {
            Contract.Requires(fieldVChar != null);
        }

        public FieldContentToken(FieldVCharToken fieldVCharLeft, RWSToken rwsToken, FieldVCharToken fieldVCharRight, ITextContext context)
            : base(string.Concat(fieldVCharLeft.Data, rwsToken.Data, fieldVCharRight.Data), context)
        {
            Contract.Requires(fieldVCharLeft != null);
            Contract.Requires(rwsToken != null);
            Contract.Requires(fieldVCharRight != null);
        }
    }
}
