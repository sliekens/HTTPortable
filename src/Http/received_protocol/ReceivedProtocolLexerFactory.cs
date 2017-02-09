using System;
using Http.protocol_name;
using Http.protocol_version;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.received_protocol
{
    public sealed class ReceivedProtocolLexerFactory : RuleLexerFactory<ReceivedProtocol>
    {
        static ReceivedProtocolLexerFactory()
        {
            Default = new ReceivedProtocolLexerFactory(
                protocol_name.ProtocolNameLexerFactory.Default.Singleton(),
                protocol_version.ProtocolVersionLexerFactory.Default.Singleton());
        }

        public ReceivedProtocolLexerFactory(
            [NotNull] ILexerFactory<ProtocolName> protocolNameLexerFactory,
            [NotNull] ILexerFactory<ProtocolVersion> protocolVersionLexerFactory)
        {
            if (protocolNameLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(protocolNameLexerFactory));
            }
            if (protocolVersionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(protocolVersionLexerFactory));
            }
            ProtocolNameLexerFactory = protocolNameLexerFactory;
            ProtocolVersionLexerFactory = protocolVersionLexerFactory;
        }

        [NotNull]
        public static ReceivedProtocolLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<ProtocolName> ProtocolNameLexerFactory { get; }

        [NotNull]
        public ILexerFactory<ProtocolVersion> ProtocolVersionLexerFactory { get; }

        public override ILexer<ReceivedProtocol> Create()
        {
            var innerLexer = Concatenation.Create(
                Option.Create(
                    Concatenation.Create(
                        ProtocolNameLexerFactory.Create(),
                        Terminal.Create(@"/", StringComparer.Ordinal))),
                ProtocolVersionLexerFactory.Create());
            return new ReceivedProtocolLexer(innerLexer);
        }
    }
}
