namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class SegmentNonZeroLengthLexerFactory : ILexerFactory<SegmentNonZeroLength>
    {
        private readonly ILexerFactory<PathCharacter> pathCharacterLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        public SegmentNonZeroLengthLexerFactory(ILexerFactory<PathCharacter> pathCharacterLexerFactory, IRepetitionLexerFactory repetitionLexerFactory)
        {
            if (pathCharacterLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(pathCharacterLexerFactory));
            }

            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }

            this.pathCharacterLexerFactory = pathCharacterLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
        }

        public ILexer<SegmentNonZeroLength> Create()
        {
            var pathCharacterLexer = pathCharacterLexerFactory.Create();
            var segmentRepetitionLexer = repetitionLexerFactory.Create(pathCharacterLexer, 1, int.MaxValue);
            return new SegmentNonZeroLengthLexer(segmentRepetitionLexer);
        }
    }
}
