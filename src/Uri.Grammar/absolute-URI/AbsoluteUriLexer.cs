namespace Uri.Grammar
{
    using System;

    using SLANG;

    public class AbsoluteUriLexer : Lexer<AbsoluteUri>
    {
        private readonly ILexer<Sequence> innerLexer;

        public AbsoluteUriLexer(ILexer<Sequence> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, out AbsoluteUri element)
        {
            Sequence result;
            if (this.innerLexer.TryRead(scanner, out result))
            {
                element = new AbsoluteUri(result);
                return true;
            }

            element = default(AbsoluteUri);
            return false;
        }
    }
}