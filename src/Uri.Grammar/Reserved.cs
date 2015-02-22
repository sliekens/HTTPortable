using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Uri.Grammar
{
    public class Reserved : Element
    {
        public Reserved(Alternative<GenDelims, SubDelims> data, ITextContext context)
            : base(data.Element.Data, context)
        {
            Contract.Requires(data != null);
            Contract.Requires(data.Element != null);
        }
    }
}