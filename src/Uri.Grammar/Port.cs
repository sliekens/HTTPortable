namespace Uri.Grammar
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    using Text.Scanning;
    using Text.Scanning.Core;

    public class Port : Element
    {
        public Port(IList<Digit> digits, ITextContext context)
            : base(string.Concat(digits), context)
        {
            Contract.Requires(digits != null);
            Contract.Requires(Contract.ForAll(digits, digit => digit != null));
        }
    }
}
