using System;
using Http.transfer_coding;
using Http.t_ranking;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.Core;

namespace Http.t_codings
{
    public class TransferCodingCollectionLexerFactory : ILexerFactory<TransferCodingCollection>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly IOptionLexerFactory optionLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        private readonly ILexer<TransferCoding> transferCodingLexer;

        private readonly ILexer<TransferCodingRank> transferCodingRankLexer;

        public TransferCodingCollectionLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IOptionLexerFactory optionLexerFactory,
            [NotNull] ILexer<TransferCoding> transferCodingLexer,
            [NotNull] ILexer<TransferCodingRank> transferCodingRankLexer)
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
            if (optionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(optionLexerFactory));
            }
            if (transferCodingLexer == null)
            {
                throw new ArgumentNullException(nameof(transferCodingLexer));
            }
            if (transferCodingRankLexer == null)
            {
                throw new ArgumentNullException(nameof(transferCodingRankLexer));
            }
            this.terminalLexerFactory = terminalLexerFactory;
            this.alternationLexerFactory = alternationLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.optionLexerFactory = optionLexerFactory;
            this.transferCodingLexer = transferCodingLexer;
            this.transferCodingRankLexer = transferCodingRankLexer;
        }

        public ILexer<TransferCodingCollection> Create()
        {
            return
                new TransferCodingCollectionLexer(
                    alternationLexerFactory.Create(
                        terminalLexerFactory.Create(@"trailers", StringComparer.OrdinalIgnoreCase),
                        concatenationLexerFactory.Create(
                            transferCodingLexer,
                            optionLexerFactory.Create(transferCodingRankLexer))));
        }
    }
}
