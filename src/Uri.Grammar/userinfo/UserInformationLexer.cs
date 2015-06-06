namespace Uri.Grammar
{
    using System;

    using SLANG;

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

        public override bool TryRead(ITextScanner scanner, out UserInformation element)
        {
            Repetition result;
            if (this.innerLexer.TryRead(scanner, out result))
            {
                element = new UserInformation(result);
                return true;
            }

            element = default(UserInformation);
            return false;
        }
    }
}