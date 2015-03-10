namespace Uri.Grammar
{
    using System.Diagnostics.Contracts;
    using SLANG;

    public class PathCharacterLexer : Lexer<PathCharacter>
    {
        private readonly ILexer<PercentEncoding> pctEncodedLexer;
        private readonly ILexer<SubcomponentsDelimiter> subDelimsLexer;
        private readonly ILexer<Unreserved> unreservedLexer;

        public PathCharacterLexer()
            : this(new UnreservedLexer(), new PercentEncodingLexer(), new SubcomponentsDelimiterLexer())
        {
        }

        public PathCharacterLexer(ILexer<Unreserved> unreservedLexer, ILexer<PercentEncoding> pctEncodedLexer, 
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

        public override bool TryRead(ITextScanner scanner, out PathCharacter element)
        {
            if (scanner.EndOfInput)
            {
                element = default(PathCharacter);
                return false;
            }

            var context = scanner.GetContext();
            Unreserved unreserved;
            if (this.unreservedLexer.TryRead(scanner, out unreserved))
            {
                element = new PathCharacter(unreserved, context);
                return true;
            }

            PercentEncoding percentEncoding;
            if (this.pctEncodedLexer.TryRead(scanner, out percentEncoding))
            {
                element = new PathCharacter(percentEncoding, context);
                return true;
            }

            SubcomponentsDelimiter subcomponentsDelimiter;
            if (this.subDelimsLexer.TryRead(scanner, out subcomponentsDelimiter))
            {
                element = new PathCharacter(subcomponentsDelimiter, context);
                return true;
            }

            foreach (var c in new[] { ':', '@' })
            {
                if (scanner.TryMatch(c))
                {
                    element = new PathCharacter(c, context);
                    return true;
                }
            }

            element = default(PathCharacter);
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