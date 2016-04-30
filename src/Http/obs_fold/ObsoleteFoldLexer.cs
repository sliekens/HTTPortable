using System;
using Txt;
using Txt.ABNF;

namespace Http.obs_fold
{
    public sealed class ObsoleteFoldLexer : Lexer<ObsoleteFold>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public ObsoleteFoldLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<ObsoleteFold> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<ObsoleteFold>.FromResult(new ObsoleteFold(result.Element));
            }
            return ReadResult<ObsoleteFold>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
