using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Uri.Grammar
{
    public class PCharLexer : Lexer<PCharToken>
    {
        private readonly ILexer<PctEncodedToken> pctEncodedLexer;
        private readonly ILexer<SubDelimsToken> subDelimsLexer;
        private readonly ILexer<UnreservedToken> unreservedLexer;

        public PCharLexer()
            : this(new UnreservedLexer(), new PctEncodedLexer(), new SubDelimsLexer())
        {
        }

        public PCharLexer(ILexer<UnreservedToken> unreservedLexer, ILexer<PctEncodedToken> pctEncodedLexer,
            ILexer<SubDelimsToken> subDelimsLexer)
        {
            Contract.Requires(unreservedLexer != null);
            Contract.Requires(pctEncodedLexer != null);
            Contract.Requires(subDelimsLexer != null);
            this.unreservedLexer = unreservedLexer;
            this.pctEncodedLexer = pctEncodedLexer;
            this.subDelimsLexer = subDelimsLexer;
        }

        public override PCharToken Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            PCharToken token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(context, "Expected 'pchar'");
        }

        public override bool TryRead(ITextScanner scanner, out PCharToken token)
        {
            if (scanner.EndOfInput)
            {
                token = default(PCharToken);
                return false;
            }

            var context = scanner.GetContext();
            UnreservedToken unreserved;
            if (this.unreservedLexer.TryRead(scanner, out unreserved))
            {
                token = new PCharToken(unreserved, context);
                return true;
            }

            PctEncodedToken pctEncoded;
            if (this.pctEncodedLexer.TryRead(scanner, out pctEncoded))
            {
                token = new PCharToken(pctEncoded, context);
                return true;
            }

            SubDelimsToken subDelims;
            if (this.subDelimsLexer.TryRead(scanner, out subDelims))
            {
                token = new PCharToken(subDelims, context);
                return true;
            }

            foreach (var c in new[] {':', '@'})
            {
                if (scanner.TryMatch(c))
                {
                    token = new PCharToken(c, context);
                    return true;
                }
            }

            token = default(PCharToken);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.unreservedLexer != null);
            Contract.Invariant(this.pctEncodedLexer != null);
            Contract.Invariant(this.subDelimsLexer != null);
        }
    }
}