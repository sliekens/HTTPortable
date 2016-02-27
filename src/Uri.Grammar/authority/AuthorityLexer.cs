namespace Uri.Grammar
{
    using System;
    using TextFx;
    using TextFx.ABNF;

    public sealed class AuthorityLexer : Lexer<Authority>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public AuthorityLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<Authority> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<Authority>.FromResult(new Authority(result.Element));
            }
            return ReadResult<Authority>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}