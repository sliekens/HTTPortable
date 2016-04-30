using System;
using Txt;
using Txt.ABNF;

namespace Http.transfer_parameter
{
    public sealed class TransferParameterLexer : Lexer<TransferParameter>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public TransferParameterLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<TransferParameter> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<TransferParameter>.FromResult(new TransferParameter(result.Element));
            }
            return
                ReadResult<TransferParameter>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
