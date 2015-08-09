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

        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out UserInformation element)
        {
            Repetition result;
            if (this.innerLexer.TryRead(scanner, null, out result))
            {
                element = new UserInformation(result);
                if (previousElementOrNull != null)
                {
                    previousElementOrNull.NextElement = element;
                    element.PreviousElement = previousElementOrNull;
                }

                return true;
            }

            element = default(UserInformation);
            return false;
        }
    }
}