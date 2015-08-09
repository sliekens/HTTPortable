namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

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

        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out PathNoScheme element)
        {
            Sequence result;
            if (this.innerLexer.TryRead(scanner, null, out result))
            {
                element = new PathNoScheme(result);
                if (previousElementOrNull != null)
                {
                    previousElementOrNull.NextElement = element;
                    element.PreviousElement = previousElementOrNull;
                }

                return true;
            }

            element = default(PathNoScheme);
            return false;
        }
    }
}