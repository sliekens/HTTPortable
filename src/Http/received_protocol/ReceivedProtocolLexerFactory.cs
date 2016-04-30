using System;
using Http.protocol_name;
using Http.protocol_version;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;

namespace Http.received_protocol
{
    public class ReceivedProtocolLexerFactory : ILexerFactory<ReceivedProtocol>
    {
        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly IOptionLexerFactory optionLexerFactory;

        private readonly ILexer<ProtocolName> protocolNameLexer;

        private readonly ILexer<ProtocolVersion> protocolVersionLexer;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public ReceivedProtocolLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IOptionLexerFactory optionLexerFactory,
            [NotNull] ILexer<ProtocolName> protocolNameLexer,
            [NotNull] ILexer<ProtocolVersion> protocolVersionLexer)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (optionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(optionLexerFactory));
            }
            if (protocolNameLexer == null)
            {
                throw new ArgumentNullException(nameof(protocolNameLexer));
            }
            if (protocolVersionLexer == null)
            {
                throw new ArgumentNullException(nameof(protocolVersionLexer));
            }
            this.terminalLexerFactory = terminalLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.optionLexerFactory = optionLexerFactory;
            this.protocolNameLexer = protocolNameLexer;
            this.protocolVersionLexer = protocolVersionLexer;
        }

        public ILexer<ReceivedProtocol> Create()
        {
            var innerLexer =
                concatenationLexerFactory.Create(
                    optionLexerFactory.Create(
                        concatenationLexerFactory.Create(
                            protocolNameLexer,
                            terminalLexerFactory.Create(@"/", StringComparer.Ordinal))),
                    protocolVersionLexer);
            return new ReceivedProtocolLexer(innerLexer);
        }
    }
}
