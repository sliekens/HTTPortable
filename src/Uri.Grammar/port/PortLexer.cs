namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class PortLexer : Lexer<Port>
    {
        private readonly ILexer<Repetition> innerLexer;

        public PortLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<Port> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<Port>.FromError(new SyntaxError
                {
                    Message = "Expected 'port'.",
                    RuleName = "port",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new Port(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<Port>.FromResult(element);
        }
    }
}