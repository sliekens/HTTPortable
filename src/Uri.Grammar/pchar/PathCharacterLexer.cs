namespace Uri.Grammar
{
    using System;

    using SLANG;

    public class PathCharacterLexer : Lexer<PathCharacter>
    {
        private readonly ILexer<Alternative> pathCharacterAlternativeLexer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathCharacterAlternativeLexer">unreserved / pct-encoded / sub-delims / ":" / "@"</param>
        public PathCharacterLexer(ILexer<Alternative> pathCharacterAlternativeLexer)
        {
            if (pathCharacterAlternativeLexer == null)
            {
                throw new ArgumentNullException("pathCharacterAlternativeLexer", "Precondition: pathCharacterAlternativeLexer != null");
            }

            this.pathCharacterAlternativeLexer = pathCharacterAlternativeLexer;
        }

        public override bool TryRead(ITextScanner scanner, out PathCharacter element)
        {
            Alternative result;
            if (this.pathCharacterAlternativeLexer.TryRead(scanner, out result))
            {
                element = new PathCharacter(result);
                return true;
            }

            element = default(PathCharacter);
            return false;
        }
    }
}