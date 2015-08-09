namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class PathAbsoluteLexer : Lexer<PathAbsolute>
    {
        private readonly ILexer<Sequence> innerLexer;

        /// <summary>
        /// </summary>
        /// <param name="innerLexer">"/" [ segment-nz *( "/" segment ) ]</param>
        public PathAbsoluteLexer(ILexer<Sequence> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out PathAbsolute element)
        {
            Sequence result;
            if (this.innerLexer.TryRead(scanner, null, out result))
            {
                element = new PathAbsolute(result);
                if (previousElementOrNull != null)
                {
                    previousElementOrNull.NextElement = element;
                    element.PreviousElement = previousElementOrNull;
                }

                return true;
            }

            element = default(PathAbsolute);
            return false;
        }
    }
}