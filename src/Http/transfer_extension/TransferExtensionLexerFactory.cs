using System;
using Http.OWS;
using Http.token;
using Http.transfer_parameter;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;

namespace Http.transfer_extension
{
    public class TransferExtensionLexerFactory : ILexerFactory<TransferExtension>
    {
        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexer<OptionalWhiteSpace> optionalWhiteSpaceLexer;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        private readonly ILexer<Token> tokenLexer;

        private readonly ILexer<TransferParameter> transferParameterLexer;

        public TransferExtensionLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] ILexer<Token> tokenLexer,
            [NotNull] ILexer<OptionalWhiteSpace> optionalWhiteSpaceLexer,
            [NotNull] ILexer<TransferParameter> transferParameterLexer)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }
            if (tokenLexer == null)
            {
                throw new ArgumentNullException(nameof(tokenLexer));
            }
            if (optionalWhiteSpaceLexer == null)
            {
                throw new ArgumentNullException(nameof(optionalWhiteSpaceLexer));
            }
            if (transferParameterLexer == null)
            {
                throw new ArgumentNullException(nameof(transferParameterLexer));
            }
            this.terminalLexerFactory = terminalLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.tokenLexer = tokenLexer;
            this.optionalWhiteSpaceLexer = optionalWhiteSpaceLexer;
            this.transferParameterLexer = transferParameterLexer;
        }

        public ILexer<TransferExtension> Create()
        {
            return
                new TransferExtensionLexer(
                    concatenationLexerFactory.Create(
                        tokenLexer,
                        repetitionLexerFactory.Create(
                            concatenationLexerFactory.Create(
                                optionalWhiteSpaceLexer,
                                terminalLexerFactory.Create(@";", StringComparer.Ordinal),
                                optionalWhiteSpaceLexer,
                                transferParameterLexer),
                            0,
                            int.MaxValue)));
        }
    }
}
