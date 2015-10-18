namespace Http.Grammar
{
    using System;

    using TextFx;

    public class ProtocolNameLexer : Lexer<ProtocolName>
    {
        private readonly ILexer<Token> innerLexer;

        public ProtocolNameLexer(ILexer<Token> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<ProtocolName> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return
                    ReadResult<ProtocolName>.FromError(
                        new SyntaxError
                        {
                            Message = "Expected 'protocol-name'.",
                            RuleName = "protocol-name",
                            Context = scanner.GetContext(),
                            InnerError = result.Error
                        });
            }

            var element = new ProtocolName(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<ProtocolName>.FromResult(element);
        }
    }
}