using System;
using Txt;
using Txt.Core;

namespace Http.Transfer_Encoding
{
    public sealed class TransferEncodingLexer : Lexer<TransferEncoding>
    {
        private readonly ILexer<RequiredDelimitedList> innerLexer;

        public TransferEncodingLexer(ILexer<RequiredDelimitedList> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<TransferEncoding> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<TransferEncoding>.FromResult(new TransferEncoding(result.Element));
            }
            return ReadResult<TransferEncoding>.FromSyntaxError(
                SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
