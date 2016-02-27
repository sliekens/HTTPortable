namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public sealed class SegmentNonZeroLengthNoColonsLexer : Lexer<SegmentNonZeroLengthNoColons>
    {
        private readonly ILexer<Repetition> innerLexer;

        public SegmentNonZeroLengthNoColonsLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<SegmentNonZeroLengthNoColons> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<SegmentNonZeroLengthNoColons>.FromResult(new SegmentNonZeroLengthNoColons(result.Element));
            }
            return ReadResult<SegmentNonZeroLengthNoColons>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }
}