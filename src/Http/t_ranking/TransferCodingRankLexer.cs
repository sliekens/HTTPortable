using System;
using Txt;
using Txt.ABNF;
using Txt.Core;

namespace Http.t_ranking
{
    public sealed class TransferCodingRankLexer : Lexer<TransferCodingRank>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public TransferCodingRankLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<TransferCodingRank> ReadImpl(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<TransferCodingRank>.FromResult(new TransferCodingRank(result.Element));
            }
            return
                ReadResult<TransferCodingRank>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
