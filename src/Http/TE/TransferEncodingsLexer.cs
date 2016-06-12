using System;
using Txt;
using Txt.Core;

namespace Http.TE
{
    public sealed class TransferEncodingsLexer : Lexer<TransferEncodings>
    {
        private readonly ILexer<OptionalDelimitedList> innerLexer;

        public TransferEncodingsLexer(ILexer<OptionalDelimitedList> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<TransferEncodings> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<TransferEncodings>.FromResult(new TransferEncodings(result.Element));
            }
            return
                ReadResult<TransferEncodings>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
