namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class RegisteredNameLexer : Lexer<RegisteredName>
    {
        private readonly ILexer<Repetition> innerLexer;

        public RegisteredNameLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<RegisteredName> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<RegisteredName>.FromError(new SyntaxError
                {
                    Message = "Expected 'reg-name'.",
                    RuleName = "reg-name",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new RegisteredName(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<RegisteredName>.FromResult(element);
        }
    }
}