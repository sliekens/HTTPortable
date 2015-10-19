namespace Http.Grammar
{
    using System;

    using TextFx;

    using Uri.Grammar;

    public class AuthorityFormLexer : Lexer<AuthorityForm>
    {
        private readonly ILexer<Authority> innerLexer;

        public AuthorityFormLexer(ILexer<Authority> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<AuthorityForm> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return
                    ReadResult<AuthorityForm>.FromError(
                        new SyntaxError
                        {
                            Message = "Expected 'authority-form'.",
                            RuleName = "authority-form",
                            Context = scanner.GetContext(),
                            InnerError = result.Error
                        });
            }

            var element = new AuthorityForm(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<AuthorityForm>.FromResult(element);
        }
    }
}