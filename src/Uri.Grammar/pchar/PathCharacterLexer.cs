namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class PathCharacterLexer : Lexer<PathCharacter>
    {
        private readonly ILexer<Alternative> innerLexer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="innerLexer">unreserved / pct-encoded / sub-delims / ":" / "@"</param>
        public PathCharacterLexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer", "Precondition: innerLexer != null");
            }

            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out PathCharacter element)
        {
            Alternative result;
            if (this.innerLexer.TryRead(scanner, null, out result))
            {
                element = new PathCharacter(result);
                if (previousElementOrNull != null)
                {
                    previousElementOrNull.NextElement = element;
                    element.PreviousElement = previousElementOrNull;
                }

                return true;
            }

            element = default(PathCharacter);
            return false;
        }
    }
}