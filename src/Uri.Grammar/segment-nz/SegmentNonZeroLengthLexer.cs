namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class SegmentNonZeroLengthLexer : Lexer<SegmentNonZeroLength>
    {
        private readonly ILexer<Repetition> innerLexer;

        /// <summary></summary>
        /// <param name="innerLexer">1*pchar</param>
        public SegmentNonZeroLengthLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer", "Precondition: innerLexer != null");
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<SegmentNonZeroLength> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<SegmentNonZeroLength>.FromError(new SyntaxError
                {
                    Message = "Expected 'segment-nz'",
                    RuleName = "segment-nz",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new SegmentNonZeroLength(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<SegmentNonZeroLength>.FromResult(element);
        }
    }
}