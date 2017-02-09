using System;
using Http.pseudonym;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.host;
using UriSyntax.port;
using UriHost = UriSyntax.host.Host;

namespace Http.received_by
{
    public sealed class ReceivedByLexerFactory : RuleLexerFactory<ReceivedBy>
    {
        static ReceivedByLexerFactory()
        {
            Default = new ReceivedByLexerFactory(
                HostLexerFactory.Default.Singleton(),
                UriSyntax.port.PortLexerFactory.Default.Singleton(),
                pseudonym.PseudonymLexerFactory.Default.Singleton());
        }

        public ReceivedByLexerFactory(
            [NotNull] ILexerFactory<UriHost> uriHostLexerFactory,
            [NotNull] ILexerFactory<Port> portLexerFactory,
            [NotNull] ILexerFactory<Pseudonym> pseudonymLexerFactory)
        {
            if (uriHostLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(uriHostLexerFactory));
            }
            if (portLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(portLexerFactory));
            }
            if (pseudonymLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(pseudonymLexerFactory));
            }
            UriHostLexerFactory = uriHostLexerFactory;
            PortLexerFactory = portLexerFactory;
            PseudonymLexerFactory = pseudonymLexerFactory;
        }

        [NotNull]
        public static ReceivedByLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<Port> PortLexerFactory { get; }

        [NotNull]
        public ILexerFactory<Pseudonym> PseudonymLexerFactory { get; }

        [NotNull]
        public ILexerFactory<UriHost> UriHostLexerFactory { get; }

        public override ILexer<ReceivedBy> Create()
        {
            var innerLexer = Alternation.Create(
                Concatenation.Create(
                    UriHostLexerFactory.Create(),
                    Option.Create(
                        Concatenation.Create(
                            Terminal.Create(@":", StringComparer.Ordinal),
                            PortLexerFactory.Create()))),
                PseudonymLexerFactory.Create());
            return new ReceivedByLexer(innerLexer);
        }
    }
}
