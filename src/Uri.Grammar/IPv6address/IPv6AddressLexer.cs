namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class IPv6AddressLexer : Lexer<IPv6Address>
    {
        private readonly ILexer<Alternative> innerLexer;

        public IPv6AddressLexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<IPv6Address> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<IPv6Address>.FromError(new SyntaxError
                {
                    Message = "Expected 'IPv6address'.",
                    RuleName = "IPv6address",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new IPv6Address(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<IPv6Address>.FromResult(element);
        }
    }
}