namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;

    using SLANG;

    public class FieldContent : Element
    {
        public FieldContent(FieldVisibleCharacter fieldVisibleCharacter, ITextContext context)
            : base(fieldVisibleCharacter.Data, context)
        {
            Contract.Requires(fieldVisibleCharacter != null);
            Contract.Requires(context != null);
        }

        public FieldContent(FieldVisibleCharacter fieldVisibleCharacterLeft, RequiredWhiteSpace requiredWhiteSpace, 
            FieldVisibleCharacter fieldVisibleCharacterRight, ITextContext context)
            : base(
                string.Concat(fieldVisibleCharacterLeft.Data, requiredWhiteSpace.Data, fieldVisibleCharacterRight.Data), 
                context)
        {
            Contract.Requires(fieldVisibleCharacterLeft != null);
            Contract.Requires(requiredWhiteSpace != null);
            Contract.Requires(fieldVisibleCharacterRight != null);
            Contract.Requires(context != null);
        }
    }
}