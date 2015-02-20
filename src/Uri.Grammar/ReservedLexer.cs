using System.Diagnostics.Contracts;
using Text.Scanning;

namespace Uri.Grammar
{
    public class ReservedLexer : Lexer<ReservedToken>
    {
        private readonly ILexer<GenDelimsToken> genDelimsLexer;
        private readonly ILexer<SubDelimsToken> subDelimsLexer;

        public ReservedLexer(ILexer<GenDelimsToken> genDelimsLexer, ILexer<SubDelimsToken> subDelimsLexer)
        {
            Contract.Requires(genDelimsLexer != null);
            Contract.Requires(subDelimsLexer != null);
            this.genDelimsLexer = genDelimsLexer;
            this.subDelimsLexer = subDelimsLexer;
        }

        public override ReservedToken Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            ReservedToken token;
            if (this.TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(context, "Expected 'reserved'");
        }

        public override bool TryRead(ITextScanner scanner, out ReservedToken token)
        {
            if (scanner.EndOfInput)
            {
                return Default(out token);
            }

            var context = scanner.GetContext();
            TokenMutex<GenDelimsToken, SubDelimsToken> data;
            GenDelimsToken genDelims;
            if (this.genDelimsLexer.TryRead(scanner, out genDelims))
            {
                data = new TokenMutex<GenDelimsToken, SubDelimsToken>(genDelims);
            }
            else
            {
                SubDelimsToken subDelims;
                if (this.subDelimsLexer.TryRead(scanner, out subDelims))
                {
                    data = new TokenMutex<GenDelimsToken, SubDelimsToken>(subDelims);
                }
                else
                {
                    return Default(out token);
                }
            }

            token = new ReservedToken(data, context);
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