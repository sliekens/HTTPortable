namespace Uri.Grammar
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    using Text.Scanning;

    public class PathAbsoluteOrEmpty : Element
    {
        private readonly bool isEmpty;

        private readonly bool isAbsolute;

        public PathAbsoluteOrEmpty(IList<Sequence<Element, Segment>> path, ITextContext context)
            : base(string.Concat(path.Select(sequence => sequence.Data)), context)
        {
            Contract.Requires(path != null);
            Contract.Requires(Contract.ForAll(path, sequence => sequence.Element1.Data == "/"));
            Contract.Requires(context != null);
            if (path.Count == 0)
            {
                this.isEmpty = true;
            }
            else
            {
                this.isAbsolute = true;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return this.isEmpty;
            }
        }

        public bool IsAbsolute
        {
            get
            {
                return this.isAbsolute;
            }
        }
    }
}
