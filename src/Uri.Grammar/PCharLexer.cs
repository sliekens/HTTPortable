using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Uri.Grammar
{
    public class PCharLexer : Lexer<PChar>
    {
        private readonly ILexer<PercentEncoding> pctEncodedLexer;
        private readonly ILexer<SubcomponentsDelimiter> subDelimsLexer;
        private readonly ILexer<Unreserved> unreservedLexer;

        public PCharLexer()
            : this(new UnreservedLexer(), new PercentEncodingLexer(), new SubcomponentsDelimiterLexer())
        {
        }

        public PCharLexer(ILexer<Unreserved> unreservedLexer, ILexer<PercentEncoding> pctEncodedLexer,
            ILexer<SubcomponentsDelimiter> subDelimsLexer)
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

            PercentEncoding percentEncoding;
            if (this.pctEncodedLexer.TryRead(scanner, out percentEncoding))
            {
                element = new PChar(percentEncoding, context);
                return true;
            }

            SubcomponentsDelimiter subcomponentsDelimiter;
            if (this.subDelimsLexer.TryRead(scanner, out subcomponentsDelimiter))
            {
                element = new PChar(subcomponentsDelimiter, context);
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