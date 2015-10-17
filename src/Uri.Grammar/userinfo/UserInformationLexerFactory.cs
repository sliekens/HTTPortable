namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class UserInformationLexerFactory : ILexerFactory<UserInformation>
    {
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ILexerFactory<PercentEncoding> percentEncodingLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        private readonly ILexerFactory<SubcomponentsDelimiter> subcomponentsDelimiterLexerFactory;

        private readonly ILexerFactory<Unreserved> unreservedLexerFactory;

        public UserInformationLexerFactory(
            IRepetitionLexerFactory repetitionLexerFactory,
            IAlternativeLexerFactory alternativeLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            ILexerFactory<Unreserved> unreservedLexerFactory,
            ILexerFactory<PercentEncoding> percentEncodingLexerFactory,
            ILexerFactory<SubcomponentsDelimiter> subcomponentsDelimiterLexerFactory)
        {
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException("repetitionLexerFactory");
            }

            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException("alternativeLexerFactory");
            }

            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException("terminalLexerFactory");
            }

            if (unreservedLexerFactory == null)
            {
                throw new ArgumentNullException("unreservedLexerFactory");
            }

            if (percentEncodingLexerFactory == null)
            {
                throw new ArgumentNullException("percentEncodingLexerFactory");
            }

            if (subcomponentsDelimiterLexerFactory == null)
            {
                throw new ArgumentNullException("subcomponentsDelimiterLexerFactory");
            }

            this.repetitionLexerFactory = repetitionLexerFactory;
            this.alternativeLexerFactory = alternativeLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.unreservedLexerFactory = unreservedLexerFactory;
            this.percentEncodingLexerFactory = percentEncodingLexerFactory;
            this.subcomponentsDelimiterLexerFactory = subcomponentsDelimiterLexerFactory;
        }

        public ILexer<UserInformation> Create()
        {
            var unreserved = this.unreservedLexerFactory.Create();
            var pctEncoding = this.percentEncodingLexerFactory.Create();
            var subDelims = this.subcomponentsDelimiterLexerFactory.Create();
            var colon = this.terminalLexerFactory.Create(@":", StringComparer.Ordinal);
            var alt = this.alternativeLexerFactory.Create(unreserved, pctEncoding, subDelims, colon);
            var innerLexer = this.repetitionLexerFactory.Create(alt, 0, int.MaxValue);
            return new UserInformationLexer(innerLexer);
        }
    }
}