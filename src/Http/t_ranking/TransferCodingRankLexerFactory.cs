using System;
using Http.OWS;
using Http.rank;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.t_ranking
{
    public sealed class TransferCodingRankLexerFactory : RuleLexerFactory<TransferCodingRank>
    {
        static TransferCodingRankLexerFactory()
        {
            Default = new TransferCodingRankLexerFactory(
                OWS.OptionalWhiteSpaceLexerFactory.Default.Singleton(),
                rank.RankLexerFactory.Default.Singleton());
        }

        public TransferCodingRankLexerFactory(
            [NotNull] ILexerFactory<OptionalWhiteSpace> optionalWhiteSpaceLexerFactory,
            [NotNull] ILexerFactory<Rank> rankLexerFactory)
        {
            if (optionalWhiteSpaceLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(optionalWhiteSpaceLexerFactory));
            }
            if (rankLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(rankLexerFactory));
            }
            OptionalWhiteSpaceLexerFactory = optionalWhiteSpaceLexerFactory;
            RankLexerFactory = rankLexerFactory;
        }

        [NotNull]
        public static TransferCodingRankLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<OptionalWhiteSpace> OptionalWhiteSpaceLexerFactory { get; }

        [NotNull]
        public ILexerFactory<Rank> RankLexerFactory { get; }

        public override ILexer<TransferCodingRank> Create()
        {
            var ows = OptionalWhiteSpaceLexerFactory.Create();
            var innerLexer = Concatenation.Create(
                ows,
                Terminal.Create(@";", StringComparer.Ordinal),
                ows,
                Terminal.Create(@"q=", StringComparer.OrdinalIgnoreCase),
                RankLexerFactory.Create());
            return new TransferCodingRankLexer(innerLexer);
        }
    }
}
