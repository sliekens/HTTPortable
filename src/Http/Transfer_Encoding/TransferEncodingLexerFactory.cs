using System;
using Http.transfer_coding;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.Transfer_Encoding
{
    public sealed class TransferEncodingLexerFactory : RuleLexerFactory<TransferEncoding>
    {
        static TransferEncodingLexerFactory()
        {
            Default = new TransferEncodingLexerFactory(
                Http.RequiredDelimitedListLexerFactory.Default,
                transfer_coding.TransferCodingLexerFactory.Default.Singleton());
        }

        public TransferEncodingLexerFactory(
            [NotNull] IRequiredDelimitedListLexerFactory requiredDelimitedListLexerFactory,
            [NotNull] ILexerFactory<TransferCoding> transferCodingLexerFactory)
        {
            if (requiredDelimitedListLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(requiredDelimitedListLexerFactory));
            }
            if (transferCodingLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(transferCodingLexerFactory));
            }
            RequiredDelimitedListLexerFactory = requiredDelimitedListLexerFactory;
            TransferCodingLexerFactory = transferCodingLexerFactory;
        }

        [NotNull]
        public static TransferEncodingLexerFactory Default { get; }

        [NotNull]
        public IRequiredDelimitedListLexerFactory RequiredDelimitedListLexerFactory { get; }

        [NotNull]
        public ILexerFactory<TransferCoding> TransferCodingLexerFactory { get; }

        public override ILexer<TransferEncoding> Create()
        {
            var innerLexer = RequiredDelimitedListLexerFactory.Create(TransferCodingLexerFactory.Create());
            return new TransferEncodingLexer(innerLexer);
        }
    }
}
