namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class RelativeReferenceLexer : Lexer<RelativeReference>
    {
        private readonly ILexer<Sequence> innerLexer;

        public RelativeReferenceLexer(ILexer<Sequence> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out RelativeReference element)
        {
            Sequence result;
            if (this.innerLexer.TryRead(scanner, null, out result))
            {
                element = new RelativeReference(result);
                if (previousElementOrNull != null)
                {
                    previousElementOrNull.NextElement = element;
                    element.PreviousElement = previousElementOrNull;
                }

                return true;
            }

            element = default(RelativeReference);
            return false;
        }
    }
}