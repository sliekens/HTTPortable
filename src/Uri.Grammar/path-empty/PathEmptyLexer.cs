namespace Uri.Grammar.path_empty
{
    using System;

    using SLANG;

    public class PathEmptyLexer : Lexer<PathEmpty>
    {
        private readonly ILexer<Repetition> repetitionLexer;

        /// <summary>
        /// </summary>
        /// <param name="repetitionLexer">0*0( pchar )</param>
        public PathEmptyLexer(ILexer<Repetition> repetitionLexer)
        {
            if (repetitionLexer == null)
            {
                throw new ArgumentNullException("repetitionLexer", "Precondition: repetitionLexer != null");
            }

            this.repetitionLexer = repetitionLexer;
        }

        public override bool TryRead(ITextScanner scanner, out PathEmpty element)
        {
            Repetition result;
            if (this.repetitionLexer.TryRead(scanner, out result))
            {
                element = new PathEmpty(result);
                return true;
            }

            element = default(PathEmpty);
            return false;
        }
    }
}