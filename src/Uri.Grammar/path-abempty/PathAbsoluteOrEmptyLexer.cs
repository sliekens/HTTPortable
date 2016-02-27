namespace Uri.Grammar
{
    using System;
    using TextFx;
    using TextFx.ABNF;

    public sealed class PathAbsoluteOrEmptyLexer : Lexer<PathAbsoluteOrEmpty>
    {
        private readonly ILexer<Repetition> innerLexer;

        public PathAbsoluteOrEmptyLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            this.innerLexer = innerLexer;
        }

        public override ReadResult<PathAbsoluteOrEmpty> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<PathAbsoluteOrEmpty>.FromResult(new PathAbsoluteOrEmpty(result.Element));
            }
            return
                ReadResult<PathAbsoluteOrEmpty>.FromSyntaxError(
                    SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}
