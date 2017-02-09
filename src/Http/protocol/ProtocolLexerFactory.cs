using System;
using Http.protocol_name;
using Http.protocol_version;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.protocol
{
    public sealed class ProtocolLexerFactory : RuleLexerFactory<Protocol>
    {
        static ProtocolLexerFactory()
        {
            Default = new ProtocolLexerFactory(
                protocol_name.ProtocolNameLexerFactory.Default.Singleton(),
                protocol_version.ProtocolVersionLexerFactory.Default.Singleton());
        }

        public ProtocolLexerFactory(
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
        public static ProtocolLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<ProtocolName> ProtocolNameLexerFactory { get; set; }

        [NotNull]
        public ILexerFactory<ProtocolVersion> ProtocolVersionLexerFactory { get; set; }

        public override ILexer<Protocol> Create()
        {
            var innerLexer = Concatenation.Create(
                ProtocolNameLexerFactory.Create(),
                Option.Create(
                    Concatenation.Create(
                        Terminal.Create(@"/", StringComparer.Ordinal),
                        ProtocolVersionLexerFactory.Create())));
            return new ProtocolLexer(innerLexer);
        }
    }
}
