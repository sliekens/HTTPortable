namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class ReservedLexer : Lexer<Reserved>
    {
        private readonly ILexer<Alternative> innerLexer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="innerLexer">gen-delims / sub-delims</param>
        public ReservedLexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer", "Precondition: innerLexer != null");
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<Reserved> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<Reserved>.FromError(new SyntaxError
                {
                    Message = "Expected 'reserved'",
                    RuleName = "reserved",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new Reserved(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<Reserved>.FromResult(element);
        }
    }
}