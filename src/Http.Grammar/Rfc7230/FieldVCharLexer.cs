using System.Diagnostics;
using System.Diagnostics.Contracts;
using Text.Scanning;
using Text.Scanning.Core;

namespace Http.Grammar.Rfc7230
{
    public class FieldVCharLexer : Lexer<FieldVCharToken>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILexer<ObsTextToken> obsTextLexer;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ILexer<VisibleCharacter> vCharLexer;

        public FieldVCharLexer()
            : this(new VisibleCharacterLexer(), new ObsTextLexer())
        {
        }

        public FieldVCharLexer(ILexer<VisibleCharacter> vCharLexer, ILexer<ObsTextToken> obsTextLexer)
        {
            Contract.Requires(vCharLexer != null);
            Contract.Requires(obsTextLexer != null);
            this.vCharLexer = vCharLexer;
            this.obsTextLexer = obsTextLexer;
        }

        public override FieldVCharToken Read(ITextScanner scanner)
        {
            FieldVCharToken element;
            var context = scanner.GetContext();
            if (TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(context, "Expected 'field-vchar'");
        }

        public override bool TryRead(ITextScanner scanner, out FieldVCharToken element)
        {
            var context = scanner.GetContext();
            VisibleCharacter vCharToken;
            if (vCharLexer.TryRead(scanner, out vCharToken))
            {
                element = new FieldVCharToken(vCharToken, context);
                return true;
            }

            ObsTextToken obsTextToken;
            if (obsTextLexer.TryRead(scanner, out obsTextToken))
            {
                element = new FieldVCharToken(obsTextToken, context);
                return true;
            }

            element = default(FieldVCharToken);
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