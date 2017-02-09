using System;
using Http.t_codings;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.TE
{
    public sealed class TransferEncodingCollectionLexerFactory : RuleLexerFactory<TransferEncodingCollection>
    {
        static TransferEncodingCollectionLexerFactory()
        {
            Default = new TransferEncodingCollectionLexerFactory(
                Http.OptionalDelimitedListLexerFactory.Default,
                t_codings.TransferCodingCollectionLexerFactory.Default.Singleton());
        }

        public TransferEncodingCollectionLexerFactory(
            [NotNull] IOptionalDelimitedListLexerFactory optionalDelimitedListLexerFactory,
            [NotNull] ILexerFactory<TransferCodingCollection> transferCodingCollectionLexerFactory)
        {
            if (optionalDelimitedListLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(optionalDelimitedListLexerFactory));
            }
            if (transferCodingCollectionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(transferCodingCollectionLexerFactory));
            }
            OptionalDelimitedListLexerFactory = optionalDelimitedListLexerFactory;
            TransferCodingCollectionLexerFactory = transferCodingCollectionLexerFactory;
        }

        [NotNull]
        public static TransferEncodingCollectionLexerFactory Default { get; }

        [NotNull]
        public IOptionalDelimitedListLexerFactory OptionalDelimitedListLexerFactory { get; }

        [NotNull]
        public ILexerFactory<TransferCodingCollection> TransferCodingCollectionLexerFactory { get; }

        public override ILexer<TransferEncodingCollection> Create()
        {
            var innerLexer = OptionalDelimitedListLexerFactory.Create(TransferCodingCollectionLexerFactory.Create());
            return new TransferEncodingCollectionLexer(innerLexer);
        }
    }
}
