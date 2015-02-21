using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    public class Token : Element
    {
        private readonly IList<TCharToken> tCharList;

        public Token(IList<TCharToken> tCharList, ITextContext context)
            : base(string.Concat(tCharList.Select(tchar => tchar.Data)), context)
        {
            Contract.Requires(tCharList != null);
            Contract.Requires(tCharList.Count > 0);
            Contract.Requires(Contract.ForAll(tCharList, tchar => tchar != null));
            this.tCharList = new ReadOnlyCollection<TCharToken>(tCharList.ToList());
        }

        public IList<TCharToken> TCharList
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
