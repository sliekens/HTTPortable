namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class DecimalOctetLexer : Lexer<DecimalOctet>
    {
        private readonly ILexer<Alternative> innerLexer;

        public DecimalOctetLexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<DecimalOctet> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<DecimalOctet>.FromError(new SyntaxError
                {
                    Message = "Expected 'dec-octet'.",
                    RuleName = "dec-octet",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new DecimalOctet(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<DecimalOctet>.FromResult(element);
        }
    }
}