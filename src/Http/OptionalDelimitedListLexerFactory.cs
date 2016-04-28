using System;
using Http.OWS;
using Txt;
using Txt.ABNF;

namespace Http
{
    public class OptionalDelimitedListLexerFactory : IOptionalDelimitedListLexerFactory
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ILexerFactory<OptionalWhiteSpace> optionalWhiteSpaceLexerFactory;

        private readonly IOptionLexerFactory optionLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public OptionalDelimitedListLexerFactory(
            IOptionLexerFactory optionLexerFactory,
            IConcatenationLexerFactory concatenationLexerFactory,
            IAlternationLexerFactory alternationLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            ILexerFactory<OptionalWhiteSpace> optionalWhiteSpaceLexerFactory,
            IRepetitionLexerFactory repetitionLexerFactory)
        {
            if (optionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(optionLexerFactory));
            }

            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }

            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }

            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }

            if (optionalWhiteSpaceLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(optionalWhiteSpaceLexerFactory));
            }

            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }

            this.optionLexerFactory = optionLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.alternationLexerFactory = alternationLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.optionalWhiteSpaceLexerFactory = optionalWhiteSpaceLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
        }

        public ILexer<OptionalDelimitedList> Create(ILexer lexer)
        {
            var delim = terminalLexerFactory.Create(@",", StringComparer.Ordinal);
            var ows = optionalWhiteSpaceLexerFactory.Create();
            var innerLexer =
                optionLexerFactory.Create(
                    concatenationLexerFactory.Create(
                        alternationLexerFactory.Create(delim, lexer),
                        repetitionLexerFactory.Create(
                            concatenationLexerFactory.Create(
                                ows,
                                delim,
                                optionLexerFactory.Create(concatenationLexerFactory.Create(ows, lexer))),
                            0,
                            int.MaxValue)));
            return new OptionalDelimitedListLexer(innerLexer);
        }
    }
}