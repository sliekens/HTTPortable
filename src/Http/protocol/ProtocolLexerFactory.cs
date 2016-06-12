using System;
using Http.protocol_name;
using Http.protocol_version;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.Core;

namespace Http.protocol
{
    public class ProtocolLexerFactory : ILexerFactory<Protocol>
    {
        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly IOptionLexerFactory optionLexerFactory;

        private readonly ILexer<ProtocolName> protocolNameLexer;

        private readonly ILexer<ProtocolVersion> protocolVersionLexer;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public ProtocolLexerFactory(
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

        public ILexer<Protocol> Create()
        {
            return
                new ProtocolLexer(
                    concatenationLexerFactory.Create(
                        protocolNameLexer,
                        optionLexerFactory.Create(
                            concatenationLexerFactory.Create(
                                terminalLexerFactory.Create(@"/", StringComparer.Ordinal),
                                protocolVersionLexer))));
        }
    }
}
