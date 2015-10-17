namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class GenericDelimiterLexer : Lexer<GenericDelimiter>
    {
        private readonly ILexer<Alternative> innerLexer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="innerLexer">":" / "/" / "?" / "#" / "[" / "]" / "@"</param>
        public GenericDelimiterLexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer", "Precondition: innerLexer != null");
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<GenericDelimiter> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<GenericDelimiter>.FromError(new SyntaxError
                {
                    Message = "Expected 'gen-delims'.",
                    RuleName = "gen-delims",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new GenericDelimiter(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<GenericDelimiter>.FromResult(element);
        }
    }
}