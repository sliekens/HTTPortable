using System.Diagnostics;
using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class FieldVisibleCharacterLexer : Lexer<FieldVisibleCharacter>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILexer<ObsoletedText> obsTextLexer;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILexer<VisibleCharacter> vCharLexer;

        public FieldVisibleCharacterLexer()
            : this(new VisibleCharacterLexer(), new ObsoletedTextLexer())
        {
        }

        public FieldVisibleCharacterLexer(ILexer<VisibleCharacter> vCharLexer, ILexer<ObsoletedText> obsTextLexer)
        {
            Contract.Requires(vCharLexer != null);
            Contract.Requires(obsTextLexer != null);
            this.vCharLexer = vCharLexer;
            this.obsTextLexer = obsTextLexer;
        }

        public override FieldVisibleCharacter Read(ITextScanner scanner)
        {
            FieldVisibleCharacter element;
            var context = scanner.GetContext();
            if (TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(context, "Expected 'field-vchar'");
        }

        public override bool TryRead(ITextScanner scanner, out FieldVisibleCharacter element)
        {
            var context = scanner.GetContext();
            VisibleCharacter vCharToken;
            if (vCharLexer.TryRead(scanner, out vCharToken))
            {
                element = new FieldVisibleCharacter(vCharToken, context);
                return true;
            }

            ObsoletedText obsoletedText;
            if (obsTextLexer.TryRead(scanner, out obsoletedText))
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
            Contract.Invariant(vCharLexer != null);
            Contract.Invariant(obsTextLexer != null);
        }
    }
}