namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class UriReferenceLexer : Lexer<UriReference>
    {
        private readonly ILexer<Alternative> innerLexer;

        public UriReferenceLexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out UriReference element)
        {
            Alternative result;
            if (this.innerLexer.TryRead(scanner, null, out result))
            {
                element = new UriReference(result);
                if (previousElementOrNull != null)
                {
                    previousElementOrNull.NextElement = element;
                    element.PreviousElement = previousElementOrNull;
                }

                return true;
            }

            element = default(UriReference);
            return false;
        }
    }
}