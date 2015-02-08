using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class WspMutex
    {
        private readonly SpToken sp;
        private readonly HTabToken hTab;

        public WspMutex(SpToken sp)
        {
            Contract.Requires(sp != null);
            this.sp = sp;
        }

        public WspMutex(HTabToken hTab)
        {
            Contract.Requires(hTab != null);
            this.hTab = hTab;
        }

        public Token Token
        {
            get
            {
                return this.sp as Token ?? this.hTab;
            }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.sp == null || this.hTab == null);
        }
    }
}