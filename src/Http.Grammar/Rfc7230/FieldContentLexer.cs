using System;
using System.Diagnostics.Contracts;


namespace Http.Grammar.Rfc7230
{
    using SLANG;

    public class FieldContentLexer : Lexer<FieldContent>
    {
        private readonly ILexer<FieldVisibleCharacter> fieldVisibleCharacterLexer;
        private readonly ILexer<RequiredWhiteSpace> requiredWhiteSpaceLexer;

        public FieldContentLexer()
            : this(new FieldVisibleCharacterLexer(), new RequiredWhiteSpaceLexer())
        {
        }

        public FieldContentLexer(ILexer<FieldVisibleCharacter> fieldVisibleCharacterLexer, ILexer<RequiredWhiteSpace> requiredWhiteSpaceLexer)
            : base("field-content")
        {
            Contract.Requires(fieldVisibleCharacterLexer != null);
            Contract.Requires(requiredWhiteSpaceLexer != null);
            this.fieldVisibleCharacterLexer = fieldVisibleCharacterLexer;
            this.requiredWhiteSpaceLexer = requiredWhiteSpaceLexer;
        }

        public override bool TryRead(ITextScanner scanner, out FieldContent element)
        {
            if (scanner.EndOfInput)
            {
                element = default(FieldContent);
                return false;
            }

            var context = scanner.GetContext();
            FieldVisibleCharacter fieldVisibleCharacterLeft;
            if (!this.fieldVisibleCharacterLexer.TryRead(scanner, out fieldVisibleCharacterLeft))
            {
                element = default(FieldContent);
                return false;
            }

            RequiredWhiteSpace requiredWhiteSpace;
            if (this.requiredWhiteSpaceLexer.TryRead(scanner, out requiredWhiteSpace))
            {
                FieldVisibleCharacter fieldVisibleCharacterRight;
                if (this.fieldVisibleCharacterLexer.TryRead(scanner, out fieldVisibleCharacterRight))
                {
                    element = new FieldContent(fieldVisibleCharacterLeft, requiredWhiteSpace, fieldVisibleCharacterRight, context);
                    return true;
                }

                scanner.PutBack(requiredWhiteSpace.Data);
            }

            element = new FieldContent(fieldVisibleCharacterLeft, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.fieldVisibleCharacterLexer != null);
            Contract.Invariant(this.requiredWhiteSpaceLexer != null);
        }

    }
}
