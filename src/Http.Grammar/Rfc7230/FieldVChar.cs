using System.Diagnostics;
using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class FieldVChar : Element
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ObsText obsText;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly VisibleCharacter visibleCharacter;

        public FieldVChar(VisibleCharacter visibleCharacter, ITextContext context)
            : base(visibleCharacter.Data, context)
        {
            Contract.Requires(visibleCharacter != null);
            this.visibleCharacter = visibleCharacter;
        }

        public FieldVChar(ObsText obsText, ITextContext context)
            : base(obsText.Data, context)
        {
            Contract.Requires(obsText != null);
            this.obsText = obsText;
        }

        public ObsText ObsText
        {
            get
            {
                return this.obsText;
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
            Contract.Invariant(this.visibleCharacter == null || this.obsText == null);
        }
    }
}