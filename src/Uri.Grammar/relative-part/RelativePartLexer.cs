namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

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

        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out RelativePart element)
        {
            Alternative result;
            if (this.innerLexer.TryRead(scanner, null, out result))
            {
                element = new RelativePart(result);
                if (previousElementOrNull != null)
                {
                    previousElementOrNull.NextElement = element;
                    element.PreviousElement = previousElementOrNull;
                }

                return true;
            }

            element = default(RelativePart);
            return false;
        }
    }
}