namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public sealed class PathRootlessLexer : Lexer<PathRootless>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public PathRootlessLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<PathRootless> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<PathRootless>.FromResult(new PathRootless(result.Element));
            }
            return ReadResult<PathRootless>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}