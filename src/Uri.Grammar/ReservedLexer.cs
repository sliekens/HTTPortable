using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Uri.Grammar
{
    public class ReservedLexer : Lexer<Reserved>
    {
        private readonly ILexer<GenericDelimiter> genDelimsLexer;
        private readonly ILexer<SubcomponentsDelimiter> subDelimsLexer;

        public ReservedLexer()
            : this(new GenericDelimiterLexer(), new SubcomponentsDelimiterLexer())
        {
        }

        public ReservedLexer(ILexer<GenericDelimiter> genDelimsLexer, ILexer<SubcomponentsDelimiter> subDelimsLexer)
            : base("reserved")
        {
            Contract.Requires(genDelimsLexer != null);
            Contract.Requires(subDelimsLexer != null);
            this.genDelimsLexer = genDelimsLexer;
            this.subDelimsLexer = subDelimsLexer;
        }

        public override bool TryRead(ITextScanner scanner, out Reserved element)
        {
            if (scanner.EndOfInput)
            {
                return Default(out element);
            }

            var context = scanner.GetContext();
            Alternative<GenericDelimiter, SubcomponentsDelimiter> data;
            GenericDelimiter genericDelimiter;
            if (this.genDelimsLexer.TryRead(scanner, out genericDelimiter))
            {
                data = new Alternative<GenericDelimiter, SubcomponentsDelimiter>(genericDelimiter, context);
            }
            else
            {
                SubcomponentsDelimiter subcomponentsDelimiter;
                if (this.subDelimsLexer.TryRead(scanner, out subcomponentsDelimiter))
                {
                    data = new Alternative<GenericDelimiter, SubcomponentsDelimiter>(subcomponentsDelimiter, context);
                }
                else
                {
                    return Default(out element);
                }
            }

            element = new Reserved(data, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.genDelimsLexer != null);
            Contract.Invariant(this.subDelimsLexer != null);
        }
    }
}