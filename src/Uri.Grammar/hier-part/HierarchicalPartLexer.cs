namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

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

        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out HierarchicalPart element)
        {
            Alternative result;
            if (this.innerLexer.TryRead(scanner, null, out result))
            {
                element = new HierarchicalPart(result);
                if (previousElementOrNull != null)
                {
                    previousElementOrNull.NextElement = element;
                    element.PreviousElement = previousElementOrNull;
                }

                return true;
            }

            element = default(HierarchicalPart);
            return false;
        }
    }
}