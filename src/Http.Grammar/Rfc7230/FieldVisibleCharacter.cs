using System.Diagnostics;
using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class FieldVisibleCharacter : Element
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ObsoletedText obsoletedText;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly VisibleCharacter visibleCharacter;

        public FieldVisibleCharacter(VisibleCharacter visibleCharacter, ITextContext context)
            : base(visibleCharacter.Data, context)
        {
            Contract.Requires(visibleCharacter != null);
            this.visibleCharacter = visibleCharacter;
        }

        public FieldVisibleCharacter(ObsoletedText obsoletedText, ITextContext context)
            : base(obsoletedText.Data, context)
        {
            Contract.Requires(obsoletedText != null);
            this.obsoletedText = obsoletedText;
        }

        public ObsoletedText ObsoletedText
        {
            get
            {
                return this.obsoletedText;
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
            Contract.Invariant(this.visibleCharacter == null || this.obsoletedText == null);
        }
    }
}