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

        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out RegisteredName element)
        {
            Repetition result;
            if (this.innerLexer.TryRead(scanner, null, out result))
            {
                element = new RegisteredName(result);
                if (previousElementOrNull != null)
                {
                    previousElementOrNull.NextElement = element;
                    element.PreviousElement = previousElementOrNull;
                }

                return true;
            }

            element = default(RegisteredName);
            return false;
        }
    }
}