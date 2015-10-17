namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class IPLiteralLexer : Lexer<IPLiteral>
    {
        private readonly ILexer<Sequence> innerLexer;

        public IPLiteralLexer(ILexer<Sequence> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<IPLiteral> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<IPLiteral>.FromError(new SyntaxError
                {
                    Message = "Expected 'IP-literal'.",
                    RuleName = "IP-literal",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new IPLiteral(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<IPLiteral>.FromResult(element);
        }
    }
}