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
        private readonly ILexer<VCharToken> vCharLexer;

        public FieldVCharLexer()
            : this(new VCharLexer(), new ObsTextLexer())
        {
        }

        public FieldVCharLexer(ILexer<VCharToken> vCharLexer, ILexer<ObsTextToken> obsTextLexer)
        {
            Contract.Requires(vCharLexer != null);
            Contract.Requires(obsTextLexer != null);
            this.vCharLexer = vCharLexer;
            this.obsTextLexer = obsTextLexer;
        }

        public override FieldVCharToken Read(ITextScanner scanner)
        {
            FieldVCharToken token;
            var context = scanner.GetContext();
            if (TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(context, "Expected 'field-vchar'");
        }

        public override bool TryRead(ITextScanner scanner, out FieldVCharToken token)
        {
            var context = scanner.GetContext();
            VCharToken vCharToken;
            if (vCharLexer.TryRead(scanner, out vCharToken))
            {
                token = new FieldVCharToken(vCharToken, context);
                return true;
            }

            ObsTextToken obsTextToken;
            if (obsTextLexer.TryRead(scanner, out obsTextToken))
            {
                token = new FieldVCharToken(obsTextToken, context);
                return true;
            }

            token = default(FieldVCharToken);
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