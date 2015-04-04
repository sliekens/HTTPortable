namespace Http.Grammar.Rfc7230
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using SLANG;

    public class FieldValue : Element
    {
        public FieldValue(IList<Alternative<FieldContent, ObsoletedFold>> data, ITextContext context)
            : base(string.Concat(data.Select(element => element.Data)), context)
        {
            Contract.Requires(data != null);
            Contract.Requires(context != null);
        }
    }
}