namespace Uri.Grammar
{
    using System;

    using SLANG;

    public class HierarchicalPartLexer : Lexer<HierarchicalPart>
    {
        private readonly ILexer<Alternative> innerLexer;

        public HierarchicalPartLexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, out HierarchicalPart element)
        {
            Alternative result;
            if (this.innerLexer.TryRead(scanner, out result))
            {
                element = new HierarchicalPart(result);
                return true;
            }

            element = default(HierarchicalPart);
            return false;
        }
    }
}