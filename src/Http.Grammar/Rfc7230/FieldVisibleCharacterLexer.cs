using System.Diagnostics;
using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class FieldVisibleCharacterLexer : Lexer<FieldVisibleCharacter>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILexer<ObsoletedText> obsoletedTextLexer;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILexer<VisibleCharacter> visibleCharLexer;

        public FieldVisibleCharacterLexer()
            : this(new VisibleCharacterLexer(), new ObsoletedTextLexer())
        {
        }

        public FieldVisibleCharacterLexer(ILexer<VisibleCharacter> visibleCharLexer, ILexer<ObsoletedText> obsoletedTextLexer)
        {
            Contract.Requires(visibleCharLexer != null);
            Contract.Requires(obsoletedTextLexer != null);
            this.visibleCharLexer = visibleCharLexer;
            this.obsoletedTextLexer = obsoletedTextLexer;
        }

        public override FieldVisibleCharacter Read(ITextScanner scanner)
        {
            FieldVisibleCharacter element;
            var context = scanner.GetContext();
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(context, "Expected 'field-vchar'");
        }

        public override bool TryRead(ITextScanner scanner, out FieldVisibleCharacter element)
        {
            if (scanner.EndOfInput)
            {
                element = default (FieldVisibleCharacter);
                return false;
            }

            var context = scanner.GetContext();
            VisibleCharacter visibleCharacter;
            if (this.visibleCharLexer.TryRead(scanner, out visibleCharacter))
            {
                element = new FieldVisibleCharacter(visibleCharacter, context);
                return true;
            }

            ObsoletedText obsoletedText;
            if (this.obsoletedTextLexer.TryRead(scanner, out obsoletedText))
            {
                element = new FieldVisibleCharacter(obsoletedText, context);
                return true;
            }

            element = default(FieldVisibleCharacter);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.visibleCharLexer != null);
            Contract.Invariant(this.obsoletedTextLexer != null);
        }
    }
}