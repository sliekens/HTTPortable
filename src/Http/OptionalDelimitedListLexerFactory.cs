using System;
using Http.OWS;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.Core;

namespace Http
{
    public class OptionalDelimitedListLexerFactory : IOptionalDelimitedListLexerFactory
    {
        static OptionalDelimitedListLexerFactory()
        {
            Default = new OptionalDelimitedListLexerFactory(
                Txt.ABNF.OptionLexerFactory.Default,
                Txt.ABNF.ConcatenationLexerFactory.Default,
                Txt.ABNF.AlternationLexerFactory.Default,
                Txt.ABNF.TerminalLexerFactory.Default,
                Txt.ABNF.RepetitionLexerFactory.Default,
                OWS.OptionalWhiteSpaceLexerFactory.Default.Singleton());
        }

        public OptionalDelimitedListLexerFactory(
            IOptionLexerFactory optionLexerFactory,
            IConcatenationLexerFactory concatenationLexerFactory,
            IAlternationLexerFactory alternationLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            IRepetitionLexerFactory repetitionLexerFactory,
            ILexerFactory<OptionalWhiteSpace> optionalWhiteSpaceLexerFactory)
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
            OptionLexerFactory = optionLexerFactory;
            ConcatenationLexerFactory = concatenationLexerFactory;
            AlternationLexerFactory = alternationLexerFactory;
            TerminalLexerFactory = terminalLexerFactory;
            OptionalWhiteSpaceLexerFactory = optionalWhiteSpaceLexerFactory;
            RepetitionLexerFactory = repetitionLexerFactory;
        }

        [NotNull]
        public static OptionalDelimitedListLexerFactory Default { get; }

        [NotNull]
        public IAlternationLexerFactory AlternationLexerFactory { get; }

        [NotNull]
        public IConcatenationLexerFactory ConcatenationLexerFactory { get; }

        [NotNull]
        public ILexerFactory<OptionalWhiteSpace> OptionalWhiteSpaceLexerFactory { get; }

        [NotNull]
        public IOptionLexerFactory OptionLexerFactory { get; }

        [NotNull]
        public IRepetitionLexerFactory RepetitionLexerFactory { get; }

        [NotNull]
        public ITerminalLexerFactory TerminalLexerFactory { get; }

        public ILexer<OptionalDelimitedList> Create(ILexer<Element> lexer)
        {
            var delim = TerminalLexerFactory.Create(@",", StringComparer.Ordinal);
            var ows = OptionalWhiteSpaceLexerFactory.Create();
            var innerLexer =
                OptionLexerFactory.Create(
                    ConcatenationLexerFactory.Create(
                        AlternationLexerFactory.Create(delim, lexer),
                        RepetitionLexerFactory.Create(
                            ConcatenationLexerFactory.Create(
                                ows,
                                delim,
                                OptionLexerFactory.Create(ConcatenationLexerFactory.Create(ows, lexer))),
                            0,
                            int.MaxValue)));
            return new OptionalDelimitedListLexer(innerLexer);
        }
    }
}
