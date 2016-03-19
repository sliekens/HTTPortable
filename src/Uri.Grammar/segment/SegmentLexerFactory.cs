namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class SegmentLexerFactory : ILexerFactory<Segment>
    {
        private readonly ILexerFactory<PathCharacter> pathCharacterLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        public SegmentLexerFactory(ILexerFactory<PathCharacter> pathCharacterLexerFactory, IRepetitionLexerFactory repetitionLexerFactory)
        {
            if (pathCharacterLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(pathCharacterLexerFactory), "Precondition: pathCharacterLexerFactory != null");
            }

            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory), "Precondition: repetitionLexerFactory != null");
            }

            this.pathCharacterLexerFactory = pathCharacterLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
        }

        public ILexer<Segment> Create()
        {
            var pathCharacterLexer = pathCharacterLexerFactory.Create();
            var segmentRepetitionLexer = repetitionLexerFactory.Create(pathCharacterLexer, 0, int.MaxValue);
            return new SegmentLexer(segmentRepetitionLexer);
        }
    }
}
