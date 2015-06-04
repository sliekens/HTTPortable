namespace Uri.Grammar
{
    using System;

    using SLANG;

    public class PathCharacterLexer : Lexer<PathCharacter>
    {
        private readonly ILexer<Alternative> pathCharacterAlternativeLexer;

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
            Element result;
            if (this.pathCharacterAlternativeLexer.TryReadElement(scanner, out result))
            {
                element = new PathCharacter(result);
                return true;
            }

            element = default(PathCharacter);
            return false;
        }
    }
}