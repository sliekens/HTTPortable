namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class ReservedLexerFactory : ILexerFactory<Reserved>
    {
        private readonly ILexerFactory<GenericDelimiter> genericDelimiterLexerFactory;

        private readonly ILexerFactory<SubcomponentsDelimiter> subcomponentsDelimiterLexerFactory;

        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        public ReservedLexerFactory(ILexerFactory<GenericDelimiter> genericDelimiterLexerFactory, ILexerFactory<SubcomponentsDelimiter> subcomponentsDelimiterLexerFactory, IAlternativeLexerFactory alternativeLexerFactory)
        {
            if (genericDelimiterLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(genericDelimiterLexerFactory));
            }

            if (subcomponentsDelimiterLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(subcomponentsDelimiterLexerFactory));
            }

            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternativeLexerFactory));
            }

            this.genericDelimiterLexerFactory = genericDelimiterLexerFactory;
            this.subcomponentsDelimiterLexerFactory = subcomponentsDelimiterLexerFactory;
            this.alternativeLexerFactory = alternativeLexerFactory;
        }

        public ILexer<Reserved> Create()
        {
            var reservedAlterativeLexer = alternativeLexerFactory.Create(
                genericDelimiterLexerFactory.Create(),
                subcomponentsDelimiterLexerFactory.Create());
            return new ReservedLexer(reservedAlterativeLexer);
        }
    }
}
