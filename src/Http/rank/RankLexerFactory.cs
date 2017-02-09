using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.ABNF.Core.DIGIT;
using Txt.Core;

namespace Http.rank
{
    public sealed class RankLexerFactory : RuleLexerFactory<Rank>
    {
        static RankLexerFactory()
        {
            Default = new RankLexerFactory(Txt.ABNF.Core.DIGIT.DigitLexerFactory.Default.Singleton());
        }

        public RankLexerFactory([NotNull] ILexerFactory<Digit> digitLexerFactory)
        {
            if (digitLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(digitLexerFactory));
            }
            DigitLexerFactory = digitLexerFactory;
        }

        [NotNull]
        public static RankLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<Digit> DigitLexerFactory { get; }

        public override ILexer<Rank> Create()
        {
            var decimalPoint = Terminal.Create(@".", StringComparer.Ordinal);
            var zero = Terminal.Create(@"0", StringComparer.Ordinal);
            var left = Concatenation.Create(
                zero,
                Option.Create(
                    Concatenation.Create(
                        decimalPoint,
                        Repetition.Create(DigitLexerFactory.Create(), 0, 3))));
            var right =
                Concatenation.Create(
                    Concatenation.Create(
                        Terminal.Create(@"1", StringComparer.Ordinal),
                        Option.Create(
                            Concatenation.Create(decimalPoint, Repetition.Create(zero, 0, 3)))));
            var innerLexer = Alternation.Create(left, right);
            return new RankLexer(innerLexer);
        }
    }
}
