namespace Http.Grammar
{
    using System;
    using TextFx;
    using TextFx.ABNF;

    public sealed class HttpNameLexer : Lexer<HttpName>
    {
        private readonly ILexer<Terminal> innerLexer;

        public HttpNameLexer(ILexer<Terminal> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<HttpName> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<HttpName>.FromResult(new HttpName(result.Element));
            }
            return ReadResult<HttpName>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
