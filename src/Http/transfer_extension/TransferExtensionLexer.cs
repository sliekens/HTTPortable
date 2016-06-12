using System;
using Txt;
using Txt.ABNF;
using Txt.Core;

namespace Http.transfer_extension
{
    public sealed class TransferExtensionLexer : Lexer<TransferExtension>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public TransferExtensionLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<TransferExtension> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<TransferExtension>.FromResult(new TransferExtension(result.Element));
            }
            return
                ReadResult<TransferExtension>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
