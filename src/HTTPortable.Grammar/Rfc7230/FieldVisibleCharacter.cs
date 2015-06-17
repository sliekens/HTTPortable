namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;

    using SLANG;
    using SLANG.Core;

    public class FieldVisibleCharacter : Element
    {
        public FieldVisibleCharacter(VisibleCharacter visibleCharacter, ITextContext context)
            : base(visibleCharacter.Data, context)
        {
            Contract.Requires(visibleCharacter != null);
            Contract.Requires(context != null);
        }

        public FieldVisibleCharacter(ObsoletedText obsoletedText, ITextContext context)
            : base(obsoletedText.Data, context)
        {
            Contract.Requires(obsoletedText != null);
            Contract.Requires(context != null);
        }
    }
}