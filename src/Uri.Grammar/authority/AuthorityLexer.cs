namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class AuthorityLexer : Lexer<Authority>
    {
        private readonly ILexer<Sequence> innerLexer;

        public AuthorityLexer(ILexer<Sequence> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out Authority element)
        {
            Sequence result;
            if (this.innerLexer.TryRead(scanner, null, out result))
            {
                element = new Authority(result);
                if (previousElementOrNull != null)
                {
                    previousElementOrNull.NextElement = element;
                    element.PreviousElement = previousElementOrNull;
                }

                return true;
            }

            element = default(Authority);
            return false;
        }
    }
}