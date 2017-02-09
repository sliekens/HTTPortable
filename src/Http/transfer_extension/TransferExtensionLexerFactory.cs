using System;
using Http.OWS;
using Http.token;
using Http.transfer_parameter;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.transfer_extension
{
    public sealed class TransferExtensionLexerFactory : RuleLexerFactory<TransferExtension>
    {
        static TransferExtensionLexerFactory()
        {
            Default = new TransferExtensionLexerFactory(
                token.TokenLexerFactory.Default.Singleton(),
                OWS.OptionalWhiteSpaceLexerFactory.Default.Singleton(),
                transfer_parameter.TransferParameterLexerFactory.Default.Singleton());
        }

        public TransferExtensionLexerFactory(
            [NotNull] ILexerFactory<Token> tokenLexerFactory,
            [NotNull] ILexerFactory<OptionalWhiteSpace> optionalWhiteSpaceLexerFactory,
            [NotNull] ILexerFactory<TransferParameter> transferParameterLexerFactory)
        {
            if (tokenLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(tokenLexerFactory));
            }
            if (optionalWhiteSpaceLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(optionalWhiteSpaceLexerFactory));
            }
            if (transferParameterLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(transferParameterLexerFactory));
            }
            TokenLexerFactory = tokenLexerFactory;
            OptionalWhiteSpaceLexerFactory = optionalWhiteSpaceLexerFactory;
            TransferParameterLexerFactory = transferParameterLexerFactory;
        }

        [NotNull]
        public static TransferExtensionLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<OptionalWhiteSpace> OptionalWhiteSpaceLexerFactory { get; set; }

        [NotNull]
        public ILexerFactory<Token> TokenLexerFactory { get; set; }

        [NotNull]
        public ILexerFactory<TransferParameter> TransferParameterLexerFactory { get; set; }

        public override ILexer<TransferExtension> Create()
        {
            var ows = OptionalWhiteSpaceLexerFactory.Create();
            var innerLexer = Concatenation.Create(
                TokenLexerFactory.Create(),
                Repetition.Create(
                    Concatenation.Create(
                        ows,
                        Terminal.Create(@";", StringComparer.Ordinal),
                        ows,
                        TransferParameterLexerFactory.Create()),
                    0,
                    int.MaxValue));
            return new TransferExtensionLexer(innerLexer);
        }
    }
}
