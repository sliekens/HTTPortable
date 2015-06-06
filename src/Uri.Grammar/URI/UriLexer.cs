namespace Uri.Grammar
{
    using System;

    using SLANG;

    public class UriLexer : Lexer<Uri>
    {
        private readonly ILexer<Sequence> innerLexer;

        public UriLexer(ILexer<Sequence> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, out Uri element)
        {
            Sequence result;
            if (this.innerLexer.TryRead(scanner, out result))
            {
                element = new Uri(result);
                return true;
            }

            element = default(Uri);
            return false;
        }
    }
}