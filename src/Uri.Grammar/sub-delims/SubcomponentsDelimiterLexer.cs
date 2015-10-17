namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class SubcomponentsDelimiterLexer : Lexer<SubcomponentsDelimiter>
    {
        private readonly ILexer<Alternative> innerLexer; 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="innerLexer">"!" / "$" / "&" / "'" / "(" / ")" / "*" / "+" / "," / ";" / "="</param>
        public SubcomponentsDelimiterLexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer", "Precondition: innerLexer != null");
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<SubcomponentsDelimiter> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<SubcomponentsDelimiter>.FromError(new SyntaxError
                {
                    Message = "Expected 'sub-delims'",
                    RuleName = "sub-delims",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new SubcomponentsDelimiter(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<SubcomponentsDelimiter>.FromResult(element);
        }
    }
}