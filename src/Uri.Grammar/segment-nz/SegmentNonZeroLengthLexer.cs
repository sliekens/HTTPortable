namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public sealed class SegmentNonZeroLengthLexer : Lexer<SegmentNonZeroLength>
    {
        private readonly ILexer<Repetition> innerLexer;

        public SegmentNonZeroLengthLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<SegmentNonZeroLength> Read(ITextScanner scanner)
        {
            if (scanner == null)
            {
                throw new ArgumentNullException(nameof(scanner));
            }
            var result = innerLexer.Read(scanner);
            if (result.Success)
            {
                return ReadResult<SegmentNonZeroLength>.FromResult((SegmentNonZeroLength) Activator.CreateInstance(typeof(SegmentNonZeroLength), result.Element));
            }
            return ReadResult<SegmentNonZeroLength>.FromSyntaxError(SyntaxError.FromReadResult(result, scanner.GetContext()));
        }
    }}