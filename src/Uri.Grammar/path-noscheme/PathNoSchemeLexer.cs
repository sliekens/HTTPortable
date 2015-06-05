namespace Uri.Grammar
{
    using System;

    using SLANG;

    public class PathNoSchemeLexer : Lexer<PathNoScheme>
    {
        private readonly ILexer<Sequence> innerLexer;

        /// <summary>
        /// </summary>
        /// <param name="innerLexer">segment-nz-nc *( "/" segment )</param>
        public PathNoSchemeLexer(ILexer<Sequence> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, out PathNoScheme element)
        {
            Sequence result;
            if (this.innerLexer.TryRead(scanner, out result))
            {
                element = new PathNoScheme(result);
                return true;
            }

            element = default(PathNoScheme);
            return false;
        }
    }
}