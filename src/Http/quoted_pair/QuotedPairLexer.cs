using System;
using Txt;
using Txt.ABNF;

namespace Http.quoted_pair
{
    public sealed class QuotedPairLexer : Lexer<QuotedPair>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public QuotedPairLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<QuotedPair> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<QuotedPair>.FromResult(new QuotedPair(result.Element));
            }
            return ReadResult<QuotedPair>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
