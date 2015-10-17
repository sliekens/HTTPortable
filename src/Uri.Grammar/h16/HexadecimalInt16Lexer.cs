namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class HexadecimalInt16Lexer : Lexer<HexadecimalInt16>
    {
        private readonly ILexer<Repetition> innerLexer;

        public HexadecimalInt16Lexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<HexadecimalInt16> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<HexadecimalInt16>.FromError(new SyntaxError
                {
                    Message = "Expected 'h16'.",
                    RuleName = "h16",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new HexadecimalInt16(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<HexadecimalInt16>.FromResult(element);
        }
    }
}