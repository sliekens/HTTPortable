namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class PathRootlessLexer : Lexer<PathRootless>
    {
        private readonly ILexer<Sequence> innerLexer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="innerLexer">segment-nz *( "/" segment )</param>
        public PathRootlessLexer(ILexer<Sequence> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, out PathRootless element)
        {
            Sequence result;
            if (this.innerLexer.TryRead(scanner, out result))
            {
                element = new PathRootless(result);
                return true;
            }

            element = default(PathRootless);
            return false;
        }
    }
}