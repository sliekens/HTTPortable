using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.port;
using UriHost = UriSyntax.host.Host;

namespace Http.Host
{
    public sealed class HostLexerFactory : RuleLexerFactory<Host>
    {
        static HostLexerFactory()
        {
            Default = new HostLexerFactory(
                UriSyntax.host.HostLexerFactory.Default.Singleton(),
                UriSyntax.port.PortLexerFactory.Default.Singleton());
        }

        public HostLexerFactory(
            [NotNull] ILexerFactory<UriHost> hostLexerFactory,
            [NotNull] ILexerFactory<Port> portLexerFactory)
        {
            if (hostLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(hostLexerFactory));
            }
            if (portLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(portLexerFactory));
            }
            UriHostLexerFactory = hostLexerFactory;
            PortLexerFactory = portLexerFactory;
        }

        [NotNull]
        public static HostLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<Port> PortLexerFactory { get; set; }

        [NotNull]
        public ILexerFactory<UriHost> UriHostLexerFactory { get; set; }

        public override ILexer<Host> Create()
        {
            var innerLexer = Concatenation.Create(
                UriHostLexerFactory.Create(),
                Option.Create(
                    Concatenation.Create(
                        Terminal.Create(@":", StringComparer.Ordinal),
                        PortLexerFactory.Create())));
            return new HostLexer(innerLexer);
        }
    }
}
