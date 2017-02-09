using System;
using Http.transfer_extension;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.transfer_coding
{
    public sealed class TransferCodingLexerFactory : RuleLexerFactory<TransferCoding>
    {
        static TransferCodingLexerFactory()
        {
            Default =
                new TransferCodingLexerFactory(transfer_extension.TransferExtensionLexerFactory.Default.Singleton());
        }

        public TransferCodingLexerFactory([NotNull] ILexerFactory<TransferExtension> transferExtensionLexerFactory)
        {
            if (transferExtensionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(transferExtensionLexerFactory));
            }
            TransferExtensionLexerFactory = transferExtensionLexerFactory;
        }

        [NotNull]
        public static TransferCodingLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<TransferExtension> TransferExtensionLexerFactory { get; }

        public override ILexer<TransferCoding> Create()
        {
            var innerLexer = Alternation.Create(
                Terminal.Create(@"chunked", StringComparer.OrdinalIgnoreCase),
                Terminal.Create(@"compress", StringComparer.OrdinalIgnoreCase),
                Terminal.Create(@"deflate", StringComparer.OrdinalIgnoreCase),
                Terminal.Create(@"gzip", StringComparer.OrdinalIgnoreCase),
                TransferExtensionLexerFactory.Create());
            return new TransferCodingLexer(innerLexer);
        }
    }
}
