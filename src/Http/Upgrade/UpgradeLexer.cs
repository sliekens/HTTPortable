using System;
using Txt;

namespace Http.Upgrade
{
    public sealed class UpgradeLexer : Lexer<Upgrade>
    {
        private readonly ILexer<RequiredDelimitedList> innerLexer;

        public UpgradeLexer(ILexer<RequiredDelimitedList> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<Upgrade> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<Upgrade>.FromResult(new Upgrade(result.Element));
            }
            return ReadResult<Upgrade>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
