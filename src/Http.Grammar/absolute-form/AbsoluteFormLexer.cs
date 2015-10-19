namespace Http.Grammar
{
    using System;

    using TextFx;

    using Uri.Grammar;

    public class AbsoluteFormLexer : Lexer<AbsoluteForm>
    {
        private readonly ILexer<AbsoluteUri> innerLexer;

        public AbsoluteFormLexer(ILexer<AbsoluteUri> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<AbsoluteForm> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return
                    ReadResult<AbsoluteForm>.FromError(
                        new SyntaxError
                        {
                            Message = "Expected 'absolute-form'.",
                            RuleName = "absolute-form",
                            Context = scanner.GetContext(),
                            InnerError = result.Error
                        });
            }

            var element = new AbsoluteForm(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<AbsoluteForm>.FromResult(element);
        }
    }
}