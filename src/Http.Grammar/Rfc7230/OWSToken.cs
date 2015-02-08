using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class OWSToken : Token
    {
        public OWSToken(IList<OWSMutex> data, ITextContext context)
            : base(string.Concat(data.Select(mutex => mutex.Token.Data)), context)
        {
            Contract.Requires(data != null);
        }

        public class OWSMutex
        {
            private readonly SpToken sp;
            private readonly HTabToken hTab;

            public OWSMutex(SpToken sp)
            {
                Contract.Requires(sp != null);
                this.sp = sp;
            }

            public OWSMutex(HTabToken hTab)
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
}