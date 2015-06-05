namespace Uri.Grammar
{
    using System;

    using SLANG;

    public class PathAbsoluteOrEmptyLexerFactory : ILexerFactory<PathAbsoluteOrEmpty>
    {
        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ILexerFactory<Segment> segmentLexerFactory;

        private readonly ISequenceLexerFactory sequenceLexerFactory;

        private readonly IStringLexerFactory stringLexerFactory;

        public PathAbsoluteOrEmptyLexerFactory(
            IRepetitionLexerFactory repetitionLexerFactory,
            ISequenceLexerFactory sequenceLexerFactory,
            IStringLexerFactory stringLexerFactory,
            ILexerFactory<Segment> segmentLexerFactory)
        {
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException("repetitionLexerFactory");
            }

            if (sequenceLexerFactory == null)
            {
                throw new ArgumentNullException("sequenceLexerFactory");
            }

            if (stringLexerFactory == null)
            {
                throw new ArgumentNullException("stringLexerFactory");
            }

            if (segmentLexerFactory == null)
            {
                throw new ArgumentNullException("segmentLexerFactory");
            }

            this.repetitionLexerFactory = repetitionLexerFactory;
            this.sequenceLexerFactory = sequenceLexerFactory;
            this.stringLexerFactory = stringLexerFactory;
            this.segmentLexerFactory = segmentLexerFactory;
        }

        public ILexer<PathAbsoluteOrEmpty> Create()
        {
            // "/"
            var a = this.stringLexerFactory.Create(@"/");

            // segment
            var b = this.segmentLexerFactory.Create();

            // "/" segment
            var c = this.sequenceLexerFactory.Create(a, b);

            // *( "/" segment )
            var d = this.repetitionLexerFactory.Create(c, 0, int.MaxValue);

            // path-abempty
            return new PathAbsoluteOrEmptyLexer(d);
        }
    }
}