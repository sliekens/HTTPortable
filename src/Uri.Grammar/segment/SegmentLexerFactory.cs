namespace Uri.Grammar.segment
{
    using System;

    using SLANG;

    public class SegmentLexerFactory : ILexerFactory<Segment>
    {
        private readonly ILexerFactory<PathCharacter> pathCharacterLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        public SegmentLexerFactory(ILexerFactory<PathCharacter> pathCharacterLexerFactory, IRepetitionLexerFactory repetitionLexerFactory)
        {
            if (pathCharacterLexerFactory == null)
            {
                throw new ArgumentNullException("pathCharacterLexerFactory", "Precondition: pathCharacterLexerFactory != null");
            }

            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException("repetitionLexerFactory", "Precondition: repetitionLexerFactory != null");
            }

            this.pathCharacterLexerFactory = pathCharacterLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
        }

        public ILexer<Segment> Create()
        {
            var pathCharacterLexer = this.pathCharacterLexerFactory.Create();
            var segmentRepetitionLexer = this.repetitionLexerFactory.Create(pathCharacterLexer, 0, int.MaxValue);
            return new SegmentLexer(segmentRepetitionLexer);
        }
    }
}
