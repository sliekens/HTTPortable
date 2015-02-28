namespace Uri.Grammar
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    using Text.Scanning;

    public class PathAbsoluteOrEmpty : Element
    {
        public PathAbsoluteOrEmpty(IList<Sequence<Element, Segment>> path, ITextContext context)
            : base(string.Concat(path.Select(sequence => sequence.Data)), context)
        {
            Contract.Requires(path != null);
            Contract.Requires(Contract.ForAll(path, sequence => sequence.Element1.Data == "/"));
            Contract.Requires(context != null);
        }
    }
}
