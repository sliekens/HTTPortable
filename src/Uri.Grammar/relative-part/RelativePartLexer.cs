namespace Uri.Grammar
{
    using System;

    using SLANG;

    public class RelativePartLexer : Lexer<RelativePart>
    {
        private readonly ILexer<Alternative> innerLexer;

        public RelativePartLexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, out RelativePart element)
        {
            Alternative result;
            if (this.innerLexer.TryRead(scanner, out result))
            {
                element = new RelativePart(result);
                return true;
            }

            element = default(RelativePart);
            return false;
        }
    }
}