using System.Diagnostics;
using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class FieldVCharToken : Token
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ObsTextToken obsTextToken;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly VCharToken vCharToken;

        public FieldVCharToken(VCharToken vCharToken, ITextContext context)
            : base(vCharToken.Data, context)
        {
            Contract.Requires(vCharToken != null);
            this.vCharToken = vCharToken;
        }

        public FieldVCharToken(ObsTextToken obsTextToken, ITextContext context)
            : base(obsTextToken.Data, context)
        {
            Contract.Requires(obsTextToken != null);
            this.obsTextToken = obsTextToken;
        }

        public ObsTextToken ObsText
        {
            get
            {
                return obsTextToken;
            }
        }

        public VCharToken VChar
        {
            get
            {
                return vCharToken;
            }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(vCharToken != null ^ obsTextToken != null);
        }
    }
}