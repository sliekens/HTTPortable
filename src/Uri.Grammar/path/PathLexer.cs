namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class PathLexer : Lexer<Path>
    {
        private readonly ILexer<Alternative> innerLexer;

        public PathLexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out Path element)
        {
            Alternative result;
            if (this.innerLexer.TryRead(scanner, null, out result))
            {
                element = new Path(result);
                if (previousElementOrNull != null)
                {
                    previousElementOrNull.NextElement = element;
                    element.PreviousElement = previousElementOrNull;
                }

                return true;
            }

            element = default(Path);
            return false;
        }
    }
}