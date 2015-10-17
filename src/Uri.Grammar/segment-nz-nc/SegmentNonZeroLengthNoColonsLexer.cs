namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class SegmentNonZeroLengthNoColonsLexer : Lexer<SegmentNonZeroLengthNoColons>
    {
        private readonly ILexer<Repetition> innerLexer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="innerLexer">1*( unreserved / pct-encoded / sub-delims / "@" )</param>
        public SegmentNonZeroLengthNoColonsLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer", "Precondition: innerLexer != null");
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<SegmentNonZeroLengthNoColons> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<SegmentNonZeroLengthNoColons>.FromError(new SyntaxError
                {
                    Message = "Expected 'segment-nz-nc'",
                    RuleName = "segment-nz-nc",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new SegmentNonZeroLengthNoColons(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<SegmentNonZeroLengthNoColons>.FromResult(element);
        }
    }
}