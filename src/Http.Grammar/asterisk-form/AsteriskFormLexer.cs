namespace Http.Grammar
{
    using System;

    using TextFx;

    public class AsteriskFormLexer : Lexer<AsteriskForm>
    {
        private readonly ILexer<Terminal> innerLexer;

        public AsteriskFormLexer(ILexer<Terminal> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<AsteriskForm> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return
                    ReadResult<AsteriskForm>.FromError(
                        new SyntaxError
                        {
                            Message = "Expected 'asterisk-form'.",
                            RuleName = "asterisk-form",
                            Context = scanner.GetContext(),
                            InnerError = result.Error
                        });
            }

            var element = new AsteriskForm(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<AsteriskForm>.FromResult(element);
        }
    }
}