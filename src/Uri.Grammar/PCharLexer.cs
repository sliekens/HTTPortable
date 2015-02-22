using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Uri.Grammar
{
    public class PCharLexer : Lexer<PChar>
    {
        private readonly ILexer<PctEncoded> pctEncodedLexer;
        private readonly ILexer<SubDelims> subDelimsLexer;
        private readonly ILexer<Unreserved> unreservedLexer;

        public PCharLexer()
            : this(new UnreservedLexer(), new PctEncodedLexer(), new SubDelimsLexer())
        {
        }

        public PCharLexer(ILexer<Unreserved> unreservedLexer, ILexer<PctEncoded> pctEncodedLexer,
            ILexer<SubDelims> subDelimsLexer)
            : base("pchar")
        {
            Contract.Requires(unreservedLexer != null);
            Contract.Requires(pctEncodedLexer != null);
            Contract.Requires(subDelimsLexer != null);
            this.unreservedLexer = unreservedLexer;
            this.pctEncodedLexer = pctEncodedLexer;
            this.subDelimsLexer = subDelimsLexer;
        }

        public override bool TryRead(ITextScanner scanner, out PChar element)
        {
            if (scanner.EndOfInput)
            {
                element = default(PChar);
                return false;
            }

            var context = scanner.GetContext();
            Unreserved unreserved;
            if (this.unreservedLexer.TryRead(scanner, out unreserved))
            {
                element = new PChar(unreserved, context);
                return true;
            }

            PctEncoded pctEncoded;
            if (this.pctEncodedLexer.TryRead(scanner, out pctEncoded))
            {
                element = new PChar(pctEncoded, context);
                return true;
            }

            SubDelims subDelims;
            if (this.subDelimsLexer.TryRead(scanner, out subDelims))
            {
                element = new PChar(subDelims, context);
                return true;
            }

            foreach (var c in new[] { ':', '@' })
            {
                if (scanner.TryMatch(c))
                {
                    element = new PChar(c, context);
                    return true;
                }
            }

            element = default(PChar);
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