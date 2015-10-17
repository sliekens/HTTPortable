namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class IPv4AddressLexer : Lexer<IPv4Address>
    {
        private readonly ILexer<Sequence> innerLexer;

        /// <summary>
        /// </summary>
        /// <param name="innerLexer">dec-octet "." dec-octet "." dec-octet "." dec-octet</param>
        public IPv4AddressLexer(ILexer<Sequence> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<IPv4Address> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<IPv4Address>.FromError(new SyntaxError
                {
                    Message = "Expected 'IPv4address'.",
                    RuleName = "IPv4address",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new IPv4Address(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<IPv4Address>.FromResult(element);
        }
    }
}