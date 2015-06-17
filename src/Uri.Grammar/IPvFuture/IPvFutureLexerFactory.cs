namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;
    using TextFx.ABNF.Core;

    public class IPvFutureLexerFactory : ILexerFactory<IPvFuture>
    {
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ILexerFactory<HexadecimalDigit> hexadecimalDigitLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ISequenceLexerFactory sequenceLexerFactory;

        private readonly IStringLexerFactory stringLexerFactory;

        private readonly ILexerFactory<SubcomponentsDelimiter> subcomponentsDelimiterLexerFactory;

        private readonly ILexerFactory<Unreserved> unreservedLexerFactory;

        public IPvFutureLexerFactory(
            IStringLexerFactory stringLexerFactory,
            IRepetitionLexerFactory repetitionLexerFactory,
            ISequenceLexerFactory sequenceLexerFactory,
            IAlternativeLexerFactory alternativeLexerFactory,
            ILexerFactory<HexadecimalDigit> hexadecimalDigitLexerFactory,
            ILexerFactory<Unreserved> unreservedLexerFactory,
            ILexerFactory<SubcomponentsDelimiter> subcomponentsDelimiterLexerFactory)
        {
            if (stringLexerFactory == null)
            {
                throw new ArgumentNullException("stringLexerFactory");
            }

            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException("repetitionLexerFactory");
            }

            if (sequenceLexerFactory == null)
            {
                throw new ArgumentNullException("sequenceLexerFactory");
            }

            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException("alternativeLexerFactory");
            }

            if (hexadecimalDigitLexerFactory == null)
            {
                throw new ArgumentNullException("hexadecimalDigitLexerFactory");
            }

            if (unreservedLexerFactory == null)
            {
                throw new ArgumentNullException("unreservedLexerFactory");
            }

            if (subcomponentsDelimiterLexerFactory == null)
            {
                throw new ArgumentNullException("subcomponentsDelimiterLexerFactory");
            }

            this.stringLexerFactory = stringLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.sequenceLexerFactory = sequenceLexerFactory;
            this.alternativeLexerFactory = alternativeLexerFactory;
            this.hexadecimalDigitLexerFactory = hexadecimalDigitLexerFactory;
            this.unreservedLexerFactory = unreservedLexerFactory;
            this.subcomponentsDelimiterLexerFactory = subcomponentsDelimiterLexerFactory;
        }

        public ILexer<IPvFuture> Create()
        {
            // "v"
            var v = this.stringLexerFactory.Create(@"v");

            // HEXDIG
            var hexdig = this.hexadecimalDigitLexerFactory.Create();

            // "."
            var dot = this.stringLexerFactory.Create(@".");

            // unreserved
            var unreserved = this.unreservedLexerFactory.Create();

            // sub-delims
            var subDelims = this.subcomponentsDelimiterLexerFactory.Create();

            // ":"
            var colon = this.stringLexerFactory.Create(@":");

            // 1*HEXDIG
            var r = this.repetitionLexerFactory.Create(hexdig, 1, int.MaxValue);

            // unreserved / sub-delims / ":"
            var a = this.alternativeLexerFactory.Create(unreserved, subDelims, colon);

            // 1*( unreserved / sub-delims / ":" )
            var s = this.repetitionLexerFactory.Create(a, 1, int.MaxValue);

            // "v" 1*HEXDIG "." 1*( unreserved / sub-delims / ":" )
            var innerLexer = this.sequenceLexerFactory.Create(v, r, dot, s);

            // IPvFuture
            return new IPvFutureLexer(innerLexer);
        }
    }
}