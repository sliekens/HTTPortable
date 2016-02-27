namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public sealed class PathLexer : Lexer<Path>
    {
        private readonly ILexer<Alternative> innerLexer;

        public PathLexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<Path> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<Path>.FromResult(new Path(result.Element));
            }
            return ReadResult<Path>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}