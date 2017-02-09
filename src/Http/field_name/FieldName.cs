using Http.token;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.field_name
{
    public class FieldName : Token
    {
        public FieldName([NotNull] Token token)
            : base(token)
        {
        }
    }
}
