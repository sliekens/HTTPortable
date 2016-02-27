namespace Uri.Grammar
{
    using System;
    using TextFx;
    using TextFx.ABNF;

    public sealed class LeastSignificantInt32Lexer : Lexer<LeastSignificantInt32>
    {
        private readonly ILexer<Alternative> innerLexer;

        public LeastSignificantInt32Lexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<LeastSignificantInt32> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<LeastSignificantInt32>.FromResult(new LeastSignificantInt32(result.Element));
            }
            return
                ReadResult<LeastSignificantInt32>.FromSyntaxError(
                    SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
