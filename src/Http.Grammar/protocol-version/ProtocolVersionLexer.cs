namespace Http.Grammar
{
    using System;

    using TextFx;

    public class ProtocolVersionLexer : Lexer<ProtocolVersion>
    {
        private readonly ILexer<Token> innerLexer;

        public ProtocolVersionLexer(ILexer<Token> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<ProtocolVersion> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return
                    ReadResult<ProtocolVersion>.FromError(
                        new SyntaxError
                        {
                            Message = "Expected 'protocol-version'.",
                            RuleName = "protocol-version",
                            Context = scanner.GetContext(),
                            InnerError = result.Error
                        });
            }

            var element = new ProtocolVersion(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<ProtocolVersion>.FromResult(element);
        }
    }
}