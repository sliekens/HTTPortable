namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;

    using SLANG;

    public class TransferCodingListItemLexer : Lexer<TransferCodingListItem>
    {
        private readonly ILexer<TransferCoding> transferCodingLexer;
        private readonly ILexer<TransferCodingRanking> transferCodingRankingLexer;

        public TransferCodingListItemLexer()
            : this(new TransferCodingLexer(), new TransferCodingRankingLexer())
        {
        }

        public TransferCodingListItemLexer(ILexer<TransferCoding> transferCodingLexer, ILexer<TransferCodingRanking> transferCodingRankingLexer)
            : base("t-codings")
        {
            Contract.Requires(transferCodingLexer != null);
            Contract.Requires(transferCodingRankingLexer != null);
            this.transferCodingLexer = transferCodingLexer;
            this.transferCodingRankingLexer = transferCodingRankingLexer;
        }

        public override bool TryRead(ITextScanner scanner, out TransferCodingListItem element)
        {
            if (scanner.EndOfInput)
            {
                element = default(TransferCodingListItem);
                return false;
            }

            var context = scanner.GetContext();
            Element literal;
            if (TryReadTerminal(scanner, "trailers", out literal))
            {
                element = new TransferCodingListItem(literal, context);
                return true;
            }

            Sequence<TransferCoding, Option<TransferCodingRanking>> transferCodingWithRanking;
            if (this.TryReadTransferCodingWithRanking(scanner, out transferCodingWithRanking))
            {
                element = new TransferCodingListItem(transferCodingWithRanking, context);
                return true;
            }

            element = default(TransferCodingListItem);
            return false;
        }

        private bool TryReadTransferCodingWithRanking(ITextScanner scanner, out Sequence<TransferCoding, Option<TransferCodingRanking>> element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Sequence<TransferCoding, Option<TransferCodingRanking>>);
                return false;
            }

            var context = scanner.GetContext();
            TransferCoding transferCoding;
            if (!this.transferCodingLexer.TryRead(scanner, out transferCoding))
            {
                element = default(Sequence<TransferCoding, Option<TransferCodingRanking>>);
                return false;
            }

            Option<TransferCodingRanking> transferCodingRanking;
            if (!this.TryReadOptionalTransferCodingRanking(scanner, out transferCodingRanking))
            {
                scanner.PutBack(transferCoding.Data);
                element = default(Sequence<TransferCoding, Option<TransferCodingRanking>>);
                return false;
            }

            element = new Sequence<TransferCoding, Option<TransferCodingRanking>>(transferCoding, transferCodingRanking, context);
            return true;
        }

        private bool TryReadOptionalTransferCodingRanking(ITextScanner scanner, out Option<TransferCodingRanking> element)
        {
            var context = scanner.GetContext();
            TransferCodingRanking transferCodingRanking;
            if (this.transferCodingRankingLexer.TryRead(scanner, out transferCodingRanking))
            {
                element = new Option<TransferCodingRanking>(transferCodingRanking, context);
            }
            else
            {
                element = new Option<TransferCodingRanking>(context);
            }

            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.transferCodingLexer != null);
            Contract.Requires(this.transferCodingRankingLexer != null);
        }
    }
}