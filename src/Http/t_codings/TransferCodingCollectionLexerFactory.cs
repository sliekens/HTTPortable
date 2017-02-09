using System;
using Http.transfer_coding;
using Http.t_ranking;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.t_codings
{
    public sealed class TransferCodingCollectionLexerFactory : RuleLexerFactory<TransferCodingCollection>
    {
        static TransferCodingCollectionLexerFactory()
        {
            Default =
                new TransferCodingCollectionLexerFactory(
                    transfer_coding.TransferCodingLexerFactory.Default.Singleton(),
                    t_ranking.TransferCodingRankLexerFactory.Default.Singleton());
        }

        public TransferCodingCollectionLexerFactory(
            [NotNull] ILexerFactory<TransferCoding> transferCodingLexerFactory,
            [NotNull] ILexerFactory<TransferCodingRank> transferCodingRankLexerFactory)
        {
            if (transferCodingLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(transferCodingLexerFactory));
            }
            if (transferCodingRankLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(transferCodingRankLexerFactory));
            }
            TransferCodingLexerFactory = transferCodingLexerFactory;
            TransferCodingRankLexerFactory = transferCodingRankLexerFactory;
        }

        [NotNull]
        public static TransferCodingCollectionLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<TransferCoding> TransferCodingLexerFactory { get; }

        [NotNull]
        public ILexerFactory<TransferCodingRank> TransferCodingRankLexerFactory { get; }

        public override ILexer<TransferCodingCollection> Create()
        {
            var innerLexer = Alternation.Create(
                Terminal.Create(@"trailers", StringComparer.OrdinalIgnoreCase),
                Concatenation.Create(
                    TransferCodingLexerFactory.Create(),
                    Option.Create(TransferCodingRankLexerFactory.Create())));
            return new TransferCodingCollectionLexer(innerLexer);
        }
    }
}
