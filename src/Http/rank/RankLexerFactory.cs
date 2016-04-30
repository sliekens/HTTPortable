using System;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.DIGIT;

namespace Http.rank
{
    public class RankLexerFactory : ILexerFactory<Rank>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexer<Digit> digitLexer;

        private readonly IOptionLexerFactory optionLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public RankLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] IOptionLexerFactory optionLexerFactory,
            [NotNull] ILexer<Digit> digitLexer)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }
            if (optionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(optionLexerFactory));
            }
            if (digitLexer == null)
            {
                throw new ArgumentNullException(nameof(digitLexer));
            }
            this.terminalLexerFactory = terminalLexerFactory;
            this.alternationLexerFactory = alternationLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.optionLexerFactory = optionLexerFactory;
            this.digitLexer = digitLexer;
        }

        public ILexer<Rank> Create()
        {
            var decimalPoint = terminalLexerFactory.Create(@".", StringComparer.Ordinal);
            var zero = terminalLexerFactory.Create(@"0", StringComparer.Ordinal);
            var left = concatenationLexerFactory.Create(
                zero,
                optionLexerFactory.Create(
                    concatenationLexerFactory.Create(
                        decimalPoint,
                        repetitionLexerFactory.Create(digitLexer, 0, 3))));
            var right =
                concatenationLexerFactory.Create(
                    concatenationLexerFactory.Create(
                        terminalLexerFactory.Create(@"1", StringComparer.Ordinal),
                        optionLexerFactory.Create(
                            concatenationLexerFactory.Create(decimalPoint, repetitionLexerFactory.Create(zero, 0, 3)))));
            var innerLexer = alternationLexerFactory.Create(left, right);
            return new RankLexer(innerLexer);
        }
    }
}
