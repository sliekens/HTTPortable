using System;
using Http.OWS;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http
{
    public class RequiredDelimitedListLexerFactory : IRequiredDelimitedListLexerFactory
    {
        static RequiredDelimitedListLexerFactory()
        {
            Default = new RequiredDelimitedListLexerFactory(
                Txt.ABNF.RepetitionLexerFactory.Default,
                Txt.ABNF.ConcatenationLexerFactory.Default,
                Txt.ABNF.OptionLexerFactory.Default,
                Txt.ABNF.TerminalLexerFactory.Default,
                OWS.OptionalWhiteSpaceLexerFactory.Default.Singleton());
        }

        public RequiredDelimitedListLexerFactory(
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IOptionLexerFactory optionLexerFactory,
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] ILexerFactory<OptionalWhiteSpace> optionalWhiteSpaceLexerFactory)
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
            RepetitionLexerFactory = repetitionLexerFactory;
            ConcatenationLexerFactory = concatenationLexerFactory;
            OptionLexerFactory = optionLexerFactory;
            TerminalLexerFactory = terminalLexerFactory;
            OptionalWhiteSpaceLexerFactory = optionalWhiteSpaceLexerFactory;
        }

        [NotNull]
        public static RequiredDelimitedListLexerFactory Default { get; }

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

        public ILexer<RequiredDelimitedList> Create(ILexer<Element> lexer)
        {
            var delim = TerminalLexerFactory.Create(@",", StringComparer.Ordinal);
            var ows = OptionalWhiteSpaceLexerFactory.Create();
            var innerLexer =
                ConcatenationLexerFactory.Create(
                    RepetitionLexerFactory.Create(ConcatenationLexerFactory.Create(delim, ows), 0, int.MaxValue),
                    lexer,
                    RepetitionLexerFactory.Create(
                        ConcatenationLexerFactory.Create(
                            ows,
                            delim,
                            OptionLexerFactory.Create(ConcatenationLexerFactory.Create(ows, lexer))),
                        0,
                        int.MaxValue));
            return new RequiredDelimitedListLexer(innerLexer);
        }
    }
}
