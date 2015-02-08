using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class HeaderFieldToken : Token
    {
        public HeaderFieldToken(FieldNameToken fieldName, OWSToken ows1, FieldValueToken fieldValue, OWSToken ows2, ITextContext context)
            : base(string.Concat(fieldName.Data, ows1.Data, fieldValue.Data, ows2.Data), context)
        {
        }
    }
}
