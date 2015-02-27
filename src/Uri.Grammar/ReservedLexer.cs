using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Uri.Grammar
{
    public class ReservedLexer : Lexer<Reserved>
    {
        private readonly ILexer<GenericDelimiter> genDelimsLexer;
        private readonly ILexer<SubDelims> subDelimsLexer;

        public ReservedLexer()
            : this(new GenericDelimiterLexer(), new SubDelimsLexer())
        {
        }

        public ReservedLexer(ILexer<GenericDelimiter> genDelimsLexer, ILexer<SubDelims> subDelimsLexer)
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
            Alternative<GenericDelimiter, SubDelims> data;
            GenericDelimiter genericDelimiter;
            if (this.genDelimsLexer.TryRead(scanner, out genericDelimiter))
            {
                data = new Alternative<GenericDelimiter, SubDelims>(genericDelimiter, context);
            }
            else
            {
                SubDelims subDelims;
                if (this.subDelimsLexer.TryRead(scanner, out subDelims))
                {
                    data = new Alternative<GenericDelimiter, SubDelims>(subDelims, context);
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