namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public sealed class PathAbsoluteLexer : Lexer<PathAbsolute>
    {
        private readonly ILexer<Concatenation> innerLexer;

        public PathAbsoluteLexer(ILexer<Concatenation> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<PathAbsolute> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<PathAbsolute>.FromResult(new PathAbsolute(result.Element));
            }
            return ReadResult<PathAbsolute>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}