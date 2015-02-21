using System.Diagnostics;
using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class FieldVCharToken : Element
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ObsTextToken obsTextToken;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly VisibleCharacter visibleCharacter;

        public FieldVCharToken(VisibleCharacter visibleCharacter, ITextContext context)
            : base(visibleCharacter.Data, context)
        {
            Contract.Requires(visibleCharacter != null);
            this.visibleCharacter = visibleCharacter;
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
                return this.obsTextToken;
            }
        }

        public VisibleCharacter VChar
        {
            get
            {
                return this.visibleCharacter;
            }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.visibleCharacter == null || this.obsTextToken == null);
        }
    }
}