namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class PercentEncodingLexer : Lexer<PercentEncoding>
    {
        private readonly ILexer<Sequence> innerLexer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="innerLexer">"%" HEXDIG HEXDIG</param>
        public PercentEncodingLexer(ILexer<Sequence> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer", "Precondition: innerLexer != null");
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<PercentEncoding> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<PercentEncoding>.FromError(new SyntaxError
                {
                    Message = "Expected 'pct-encoded'.",
                    RuleName = "pct-encoded",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new PercentEncoding(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<PercentEncoding>.FromResult(element);
        }
    }
}