namespace Uri.Grammar
{
    using SLANG;

    public class SegmentRepetitionLexer : RepetitionLexer
    {
        /// <summary></summary>
        /// <param name="pathCharacterLexer">*pchar</param>
        public SegmentRepetitionLexer(ILexer<PathCharacter> pathCharacterLexer)
            : base(pathCharacterLexer, 0, int.MaxValue)
        {
        }
    }
}