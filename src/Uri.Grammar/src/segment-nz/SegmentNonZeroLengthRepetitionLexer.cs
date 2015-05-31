namespace Uri.Grammar.segment_nz
{
    using SLANG;

    public class SegmentNonZeroLengthRepetitionLexer : RepetitionLexer
    {
        /// <summary></summary>
        /// <param name="pathCharacterLexer">pchar</param>
        public SegmentNonZeroLengthRepetitionLexer(ILexer<PathCharacter> pathCharacterLexer)
            : base(pathCharacterLexer, 1, int.MaxValue)
        {
        }
    }
}
