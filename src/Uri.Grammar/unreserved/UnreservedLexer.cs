namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class UnreservedLexer : Lexer<Unreserved>
    {
        private readonly ILexer<Alternative> innerLexer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="innerLexer">ALPHA / DIGIT / "-" / "." / "_" / "~"</param>
        public UnreservedLexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer", "Precondition: innerLexer != null");
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<Unreserved> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<Unreserved>.FromError(new SyntaxError
                {
                    Message = "Expected 'unreserved'",
                    RuleName = "unreserved",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new Unreserved(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<Unreserved>.FromResult(element);
        }
    }
}