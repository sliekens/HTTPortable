using System.Diagnostics;
using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class FieldVCharLexer : Lexer<FieldVChar>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILexer<ObsText> obsTextLexer;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILexer<VisibleCharacter> vCharLexer;

        public FieldVCharLexer()
            : this(new VisibleCharacterLexer(), new ObsTextLexer())
        {
        }

        public FieldVCharLexer(ILexer<VisibleCharacter> vCharLexer, ILexer<ObsText> obsTextLexer)
        {
            Contract.Requires(vCharLexer != null);
            Contract.Requires(obsTextLexer != null);
            this.vCharLexer = vCharLexer;
            this.obsTextLexer = obsTextLexer;
        }

        public override FieldVChar Read(ITextScanner scanner)
        {
            FieldVChar element;
            var context = scanner.GetContext();
            if (TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(context, "Expected 'field-vchar'");
        }

        public override bool TryRead(ITextScanner scanner, out FieldVChar element)
        {
            var context = scanner.GetContext();
            VisibleCharacter vCharToken;
            if (vCharLexer.TryRead(scanner, out vCharToken))
            {
                element = new FieldVChar(vCharToken, context);
                return true;
            }

            ObsText obsText;
            if (obsTextLexer.TryRead(scanner, out obsText))
            {
                element = new FieldVChar(obsText, context);
                return true;
            }

            element = default(FieldVChar);
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