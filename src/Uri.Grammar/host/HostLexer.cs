namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class HostLexer : Lexer<Host>
    {
        private readonly ILexer<Alternative> innerLexer;

        public HostLexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<Host> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<Host>.FromError(new SyntaxError
                {
                    Message = "Expected 'host'.",
                    RuleName = "host",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new Host(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<Host>.FromResult(element);
        }
    }
}