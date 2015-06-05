namespace Uri.Grammar
{
    using System;

    using SLANG;

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

        public override bool TryRead(ITextScanner scanner, out RegisteredName element)
        {
            Repetition result;
            if (this.innerLexer.TryRead(scanner, out result))
            {
                element = new RegisteredName(result);
                return true;
            }

            element = default(RegisteredName);
            return false;
        }
    }
}