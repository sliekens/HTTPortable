using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class TokenToken : Token
    {
        private readonly IList<tchar> tcharList;

        public TokenToken(IList<tchar> tcharList, ITextContext context)
            : base(string.Concat(tcharList.Select(tchar => tchar.Data)), context)
        {
            Contract.Requires(tcharList != null);
            Contract.Requires(tcharList.Count > 0);
            Contract.Requires(Contract.ForAll(tcharList, tchar => tchar != null));
            this.tcharList = new ReadOnlyCollection<tchar>(tcharList.ToList());
        }

        public IList<tchar> TCharList
        {
            get
            {
                return this.tcharList;
            }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.tcharList != null);
        }

    }
}
