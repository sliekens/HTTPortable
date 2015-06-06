namespace Uri.Grammar
{
    using System;

    using SLANG;

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

        public override bool TryRead(ITextScanner scanner, out UriReference element)
        {
            Alternative result;
            if (this.innerLexer.TryRead(scanner, out result))
            {
                element = new UriReference(result);
                return true;
            }

            element = default(UriReference);
            return false;
        }
    }
}