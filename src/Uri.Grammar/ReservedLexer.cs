using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Uri.Grammar
{
    public class ReservedLexer : Lexer<Reserved>
    {
        private readonly ILexer<GenDelims> genDelimsLexer;
        private readonly ILexer<SubDelims> subDelimsLexer;

        public ReservedLexer(ILexer<GenDelims> genDelimsLexer, ILexer<SubDelims> subDelimsLexer)
        {
            Contract.Requires(genDelimsLexer != null);
            Contract.Requires(subDelimsLexer != null);
            this.genDelimsLexer = genDelimsLexer;
            this.subDelimsLexer = subDelimsLexer;
        }

        public override Reserved Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            Reserved element;
            if (this.TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(context, "Expected 'reserved'");
        }

        public override bool TryRead(ITextScanner scanner, out Reserved element)
        {
            if (scanner.EndOfInput)
            {
                return Default(out element);
            }

            var context = scanner.GetContext();
            Alternative<GenDelims, SubDelims> data;
            GenDelims genDelims;
            if (this.genDelimsLexer.TryRead(scanner, out genDelims))
            {
                data = new Alternative<GenDelims, SubDelims>(genDelims, context);
            }
            else
            {
                SubDelims subDelims;
                if (this.subDelimsLexer.TryRead(scanner, out subDelims))
                {
                    data = new Alternative<GenDelims, SubDelims>(subDelims, context);
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