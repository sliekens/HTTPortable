namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;

    using SLANG;

    public class TransferCodingRankingLexer : Lexer<TransferCodingRanking>
    {
        private readonly ILexer<OptionalWhiteSpace> optionalWhiteSpaceLexer;
        private readonly ILexer<Rank> rankLexer;

        public TransferCodingRankingLexer()
            : this(new OptionalWhiteSpaceLexer(), new RankLexer())
        {
        }

        public TransferCodingRankingLexer(ILexer<OptionalWhiteSpace> optionalWhiteSpaceLexer, ILexer<Rank> rankLexer)
            : base("t-ranking")
        {
            Contract.Requires(optionalWhiteSpaceLexer != null);
            Contract.Requires(rankLexer != null);
            this.optionalWhiteSpaceLexer = optionalWhiteSpaceLexer;
            this.rankLexer = rankLexer;
        }

        public override bool TryRead(ITextScanner scanner, out TransferCodingRanking element)
        {
            if (scanner.EndOfInput)
            {
                element = default(TransferCodingRanking);
                return false;
            }

            var context = scanner.GetContext();
            OptionalWhiteSpace optionalWhiteSpace1;
            if (!this.optionalWhiteSpaceLexer.TryRead(scanner, out optionalWhiteSpace1))
            {
                element = default(TransferCodingRanking);
                return false;
            }

            Element semicolon;
            if (!TryReadTerminal(scanner, ";", out semicolon))
            {
                scanner.PutBack(optionalWhiteSpace1.Data);
                element = default(TransferCodingRanking);
                return false;
            }

            OptionalWhiteSpace optionalWhiteSpace2;
            if (!this.optionalWhiteSpaceLexer.TryRead(scanner, out optionalWhiteSpace2))
            {
                scanner.PutBack(semicolon.Data);
                scanner.PutBack(optionalWhiteSpace1.Data);
                element = default(TransferCodingRanking);
                return false;
            }

            Element qualityParameter;
            if (!TryReadTerminal(scanner, "q=", out qualityParameter))
            {
                scanner.PutBack(optionalWhiteSpace2.Data);
                scanner.PutBack(semicolon.Data);
                scanner.PutBack(optionalWhiteSpace1.Data);
                element = default(TransferCodingRanking);
                return false;
            }

            Rank rank;
            if (!this.rankLexer.TryRead(scanner, out rank))
            {
                scanner.PutBack(qualityParameter.Data);
                scanner.PutBack(optionalWhiteSpace2.Data);
                scanner.PutBack(semicolon.Data);
                scanner.PutBack(optionalWhiteSpace1.Data);
                element = default(TransferCodingRanking);
                return false;
            }

            element = new TransferCodingRanking(optionalWhiteSpace1, semicolon, optionalWhiteSpace2, qualityParameter, rank, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.optionalWhiteSpaceLexer != null);
            Contract.Invariant(this.rankLexer != null);
        }
    }
}