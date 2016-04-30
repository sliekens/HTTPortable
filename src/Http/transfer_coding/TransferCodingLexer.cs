using System;
using Txt;
using Txt.ABNF;

namespace Http.transfer_coding
{
    public sealed class TransferCodingLexer : Lexer<TransferCoding>
    {
        private readonly ILexer<Alternation> innerLexer;

        public TransferCodingLexer(ILexer<Alternation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<TransferCoding> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<TransferCoding>.FromResult(new TransferCoding(result.Element));
            }
            return ReadResult<TransferCoding>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
