using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class Token : Element
    {
        private readonly IList<TChar> tCharList;

        public Token(IList<TChar> tCharList, ITextContext context)
            : base(string.Concat(tCharList.Select(tchar => tchar.Data)), context)
        {
            Contract.Requires(tCharList != null);
            Contract.Requires(tCharList.Count > 0);
            Contract.Requires(Contract.ForAll(tCharList, tchar => tchar != null));
            this.tCharList = new ReadOnlyCollection<TChar>(tCharList.ToList());
        }

        public IList<TChar> TCharList
        {
            get
            {
                return this.tCharList;
            }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.tCharList != null);
        }

    }
}
