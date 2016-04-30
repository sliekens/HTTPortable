using System;
using Txt;
using Txt.ABNF;

namespace Http.rank
{
    public sealed class RankLexer : Lexer<Rank>
    {
        private readonly ILexer<Alternation> innerLexer;

        public RankLexer(ILexer<Alternation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<Rank> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<Rank>.FromResult(new Rank(result.Element));
            }
            return ReadResult<Rank>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
