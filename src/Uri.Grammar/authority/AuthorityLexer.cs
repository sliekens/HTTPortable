namespace Uri.Grammar
{
    using System;

    using SLANG;

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

        public override bool TryRead(ITextScanner scanner, out Authority element)
        {
            Sequence result;
            if (this.innerLexer.TryRead(scanner, out result))
            {
                element = new Authority(result);
                return true;
            }

            element = default(Authority);
            return false;
        }
    }
}