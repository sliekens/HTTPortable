﻿namespace Uri.Grammar.segment_nz
{
    using System;

    using SLANG;

    public class SegmentNonZeroLengthLexerFactory : ILexerFactory<SegmentNonZeroLength>
    {
        private readonly ILexerFactory<PathCharacter> pathCharacterLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        public SegmentNonZeroLengthLexerFactory(ILexerFactory<PathCharacter> pathCharacterLexerFactory, IRepetitionLexerFactory repetitionLexerFactory)
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

        public ILexer<SegmentNonZeroLength> Create()
        {
            var pathCharacterLexer = this.pathCharacterLexerFactory.Create();
            var segmentRepetitionLexer = this.repetitionLexerFactory.Create(pathCharacterLexer, 1, int.MaxValue);
            return new SegmentNonZeroLengthLexer(segmentRepetitionLexer);
        }
    }
}