namespace Uri.Grammar
{
    using System;
    using TextFx;
    using TextFx.ABNF;

    public sealed class AbsoluteUriLexer : Lexer<AbsoluteUri>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public AbsoluteUriLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<AbsoluteUri> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<AbsoluteUri>.FromResult(new AbsoluteUri(result.Element));
            }
            return ReadResult<AbsoluteUri>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
