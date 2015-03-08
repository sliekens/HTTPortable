namespace Uri.Grammar
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    using SLANG;

    public class RegisteredName : Element
    {
        public RegisteredName(IList<Alternative<Unreserved, PercentEncoding, SubcomponentsDelimiter>> elements, ITextContext context)
            : base(string.Concat(elements.Select(element => element.Data)), context)
        {
            Contract.Requires(elements != null);
            Contract.Requires(Contract.ForAll(elements, element => element != null));
            Contract.Requires(context != null);
        }
    }
}
