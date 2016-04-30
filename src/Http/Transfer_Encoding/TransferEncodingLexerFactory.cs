using System;
using Http.transfer_coding;
using JetBrains.Annotations;
using Txt;

namespace Http.Transfer_Encoding
{
    public class TransferEncodingLexerFactory : ILexerFactory<TransferEncoding>
    {
        private readonly IRequiredDelimitedListLexerFactory requiredDelimitedListLexerFactory;

        private readonly ILexer<TransferCoding> transferCodingLexer;

        public TransferEncodingLexerFactory(
            [NotNull] IRequiredDelimitedListLexerFactory requiredDelimitedListLexerFactory,
            [NotNull] ILexer<TransferCoding> transferCodingLexer)
        {
            if (requiredDelimitedListLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(requiredDelimitedListLexerFactory));
            }
            if (transferCodingLexer == null)
            {
                throw new ArgumentNullException(nameof(transferCodingLexer));
            }
            this.requiredDelimitedListLexerFactory = requiredDelimitedListLexerFactory;
            this.transferCodingLexer = transferCodingLexer;
        }

        public ILexer<TransferEncoding> Create()
        {
            return new TransferEncodingLexer(requiredDelimitedListLexerFactory.Create(transferCodingLexer));
        }
    }
}
