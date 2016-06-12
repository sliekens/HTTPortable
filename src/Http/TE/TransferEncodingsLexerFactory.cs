using System;
using Http.t_codings;
using JetBrains.Annotations;
using Txt;
using Txt.Core;

namespace Http.TE
{
    public class TransferEncodingsLexerFactory : ILexerFactory<TransferEncodings>
    {
        private readonly IOptionalDelimitedListLexerFactory optionalDelimitedListLexerFactory;

        private readonly ILexer<TransferCodingCollection> transferCodingCollectionLexer;

        public TransferEncodingsLexerFactory(
            [NotNull] IOptionalDelimitedListLexerFactory optionalDelimitedListLexerFactory,
            [NotNull] ILexer<TransferCodingCollection> transferCodingCollectionLexer)
        {
            if (optionalDelimitedListLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(optionalDelimitedListLexerFactory));
            }
            if (transferCodingCollectionLexer == null)
            {
                throw new ArgumentNullException(nameof(transferCodingCollectionLexer));
            }
            this.optionalDelimitedListLexerFactory = optionalDelimitedListLexerFactory;
            this.transferCodingCollectionLexer = transferCodingCollectionLexer;
        }

        public ILexer<TransferEncodings> Create()
        {
            return new TransferEncodingsLexer(optionalDelimitedListLexerFactory.Create(transferCodingCollectionLexer));
        }
    }
}
