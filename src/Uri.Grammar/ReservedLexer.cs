using System.Diagnostics.Contracts;


namespace Uri.Grammar
{
    using SLANG;

    public class ReservedLexer : Lexer<Reserved>
    {
        private readonly ILexer<GenericDelimiter> genericDelimiterLexer;
        private readonly ILexer<SubcomponentsDelimiter> subcomponentsDelimiterLexer;

        public ReservedLexer()
            : this(new GenericDelimiterLexer(), new SubcomponentsDelimiterLexer())
        {
        }

        public ReservedLexer(ILexer<GenericDelimiter> genericDelimiterLexer, ILexer<SubcomponentsDelimiter> subcomponentsDelimiterLexer)
            : base("reserved")
        {
            Contract.Requires(genericDelimiterLexer != null);
            Contract.Requires(subcomponentsDelimiterLexer != null);
            this.genericDelimiterLexer = genericDelimiterLexer;
            this.subcomponentsDelimiterLexer = subcomponentsDelimiterLexer;
        }

        public override bool TryRead(ITextScanner scanner, out Reserved element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Reserved);
                return false;
            }

            var context = scanner.GetContext();
            GenericDelimiter genericDelimiter;
            if (this.genericDelimiterLexer.TryRead(scanner, out genericDelimiter))
            {
                element = new Reserved(genericDelimiter, context);
                return true;
            }

            SubcomponentsDelimiter subcomponentsDelimiter;
            if (this.subcomponentsDelimiterLexer.TryRead(scanner, out subcomponentsDelimiter))
            {
                element = new Reserved(subcomponentsDelimiter, context);
                return true;
            }

            element = default(Reserved);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.genericDelimiterLexer != null);
            Contract.Invariant(this.subcomponentsDelimiterLexer != null);
        }
    }
}