using System;
using Http.OWS;
using Txt;
using Txt.ABNF;
using Txt.Core;

namespace Http
{
    public class RequiredDelimitedListLexerFactory : IRequiredDelimitedListLexerFactory
    {
        private readonly ILexerFactory<OptionalWhiteSpace> optionalWhiteSpaceLexerFactory;

        private readonly IOptionLexerFactory optionLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public RequiredDelimitedListLexerFactory(
            IRepetitionLexerFactory repetitionLexerFactory,
            IConcatenationLexerFactory concatenationLexerFactory,
            IOptionLexerFactory optionLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            ILexerFactory<OptionalWhiteSpace> optionalWhiteSpaceLexerFactory)
        {
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }

            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }

            if (optionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(optionLexerFactory));
            }

            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }

            if (optionalWhiteSpaceLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(optionalWhiteSpaceLexerFactory));
            }

            this.repetitionLexerFactory = repetitionLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.optionLexerFactory = optionLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.optionalWhiteSpaceLexerFactory = optionalWhiteSpaceLexerFactory;
        }

        public ILexer<RequiredDelimitedList> Create(ILexer lexer)
        {
            var delim = terminalLexerFactory.Create(@",", StringComparer.Ordinal);
            var ows = optionalWhiteSpaceLexerFactory.Create();
            var innerLexer =
                concatenationLexerFactory.Create(
                    repetitionLexerFactory.Create(concatenationLexerFactory.Create(delim, ows), 0, int.MaxValue),
                    lexer,
                    repetitionLexerFactory.Create(
                        concatenationLexerFactory.Create(
                            ows,
                            delim,
                            optionLexerFactory.Create(concatenationLexerFactory.Create(ows, lexer))),
                        0,
                        int.MaxValue));

            return new RequiredDelimitedListLexer(innerLexer);
        }
    }
}