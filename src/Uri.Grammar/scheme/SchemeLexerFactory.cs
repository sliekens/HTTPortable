namespace Uri.Grammar
{
    using System;

    using SLANG;
    using SLANG.Core;

    public class SchemeLexerFactory : ILexerFactory<Scheme>
    {
        private readonly ILexerFactory<Alpha> alphaLexerFactory;

        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ILexerFactory<Digit> digitLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ISequenceLexerFactory sequenceLexerFactory;

        private readonly IStringLexerFactory stringLexerFactory;

        public SchemeLexerFactory(
            ISequenceLexerFactory sequenceLexerFactory,
            IAlternativeLexerFactory alternativeLexerFactory,
            IRepetitionLexerFactory repetitionLexerFactory,
            ILexerFactory<Alpha> alphaLexerFactory,
            ILexerFactory<Digit> digitLexerFactory,
            IStringLexerFactory stringLexerFactory)
        {
            if (sequenceLexerFactory == null)
            {
                throw new ArgumentNullException("sequenceLexerFactory");
            }

            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException("alternativeLexerFactory");
            }

            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException("repetitionLexerFactory");
            }

            if (alphaLexerFactory == null)
            {
                throw new ArgumentNullException("alphaLexerFactory");
            }

            if (digitLexerFactory == null)
            {
                throw new ArgumentNullException("digitLexerFactory");
            }

            if (stringLexerFactory == null)
            {
                throw new ArgumentNullException("stringLexerFactory");
            }

            this.sequenceLexerFactory = sequenceLexerFactory;
            this.alternativeLexerFactory = alternativeLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.alphaLexerFactory = alphaLexerFactory;
            this.digitLexerFactory = digitLexerFactory;
            this.stringLexerFactory = stringLexerFactory;
        }

        public ILexer<Scheme> Create()
        {
            var alpha = this.alphaLexerFactory.Create();
            var digit = this.digitLexerFactory.Create();
            var plus = this.stringLexerFactory.Create(@"+");
            var minus = this.stringLexerFactory.Create(@"-");
            var dot = this.stringLexerFactory.Create(@".");
            var alt = this.alternativeLexerFactory.Create(alpha, digit, plus, minus, dot);
            var rep = this.repetitionLexerFactory.Create(alt, 0, int.MaxValue);
            var innerLexer = this.sequenceLexerFactory.Create(alpha, rep);
            return new SchemeLexer(innerLexer);
        }
    }
}