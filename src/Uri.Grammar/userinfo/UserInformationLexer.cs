namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class UserInformationLexer : Lexer<UserInformation>
    {
        private readonly ILexer<Repetition> innerLexer;

        public UserInformationLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<UserInformation> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<UserInformation>.FromError(new SyntaxError
                {
                    Message = "Expected 'userinfo'",
                    RuleName = "userinfo",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new UserInformation(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<UserInformation>.FromResult(element);
        }
    }
}