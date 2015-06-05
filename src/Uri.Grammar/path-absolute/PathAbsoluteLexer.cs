namespace Uri.Grammar
{
    using System;

    using SLANG;

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

        public override bool TryRead(ITextScanner scanner, out PathAbsolute element)
        {
            Sequence result;
            if (this.innerLexer.TryRead(scanner, out result))
            {
                element = new PathAbsolute(result);
                return true;
            }

            element = default(PathAbsolute);
            return false;
        }
    }
}