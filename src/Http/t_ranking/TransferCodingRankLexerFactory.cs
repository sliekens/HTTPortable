using System;
using Http.OWS;
using Http.rank;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.Core;

namespace Http.t_ranking
{
    public class TransferCodingRankLexerFactory : ILexerFactory<TransferCodingRank>
    {
        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexer<OptionalWhiteSpace> optionalWhiteSpaceLexer;

        private readonly ILexer<Rank> rankLexer;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public TransferCodingRankLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] ILexer<OptionalWhiteSpace> optionalWhiteSpaceLexer,
            [NotNull] ILexer<Rank> rankLexer)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (optionalWhiteSpaceLexer == null)
            {
                throw new ArgumentNullException(nameof(optionalWhiteSpaceLexer));
            }
            if (rankLexer == null)
            {
                throw new ArgumentNullException(nameof(rankLexer));
            }
            this.terminalLexerFactory = terminalLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.optionalWhiteSpaceLexer = optionalWhiteSpaceLexer;
            this.rankLexer = rankLexer;
        }

        public ILexer<TransferCodingRank> Create()
        {
            return
                new TransferCodingRankLexer(
                    concatenationLexerFactory.Create(
                        optionalWhiteSpaceLexer,
                        terminalLexerFactory.Create(@";", StringComparer.Ordinal),
                        optionalWhiteSpaceLexer,
                        terminalLexerFactory.Create(@"q=", StringComparer.OrdinalIgnoreCase),
                        rankLexer));
        }
    }
}
