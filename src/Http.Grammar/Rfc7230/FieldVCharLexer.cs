using System.Diagnostics;
using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class FieldVCharLexer : Lexer<FieldVChar>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILexer<ObsoletedText> obsTextLexer;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILexer<VisibleCharacter> vCharLexer;

        public FieldVCharLexer()
            : this(new VisibleCharacterLexer(), new ObsoletedTextLexer())
        {
        }

        public FieldVCharLexer(ILexer<VisibleCharacter> vCharLexer, ILexer<ObsoletedText> obsTextLexer)
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

            ObsoletedText obsoletedText;
            if (obsTextLexer.TryRead(scanner, out obsoletedText))
            {
                element = new FieldVChar(obsoletedText, context);
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