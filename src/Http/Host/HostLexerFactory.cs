using System;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Uri.port;

namespace Http.Host
{
    public class HostLexerFactory : ILexerFactory<Host>
    {
        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexer<Uri.host.Host> hostLexer;

        private readonly IOptionLexerFactory optionLexerFactory;

        private readonly ILexer<Port> portLexer;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public HostLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IOptionLexerFactory optionLexerFactory,
            [NotNull] ILexer<Uri.host.Host> hostLexer,
            [NotNull] ILexer<Port> portLexer)
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
            if (hostLexer == null)
            {
                throw new ArgumentNullException(nameof(hostLexer));
            }
            if (portLexer == null)
            {
                throw new ArgumentNullException(nameof(portLexer));
            }
            this.terminalLexerFactory = terminalLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.optionLexerFactory = optionLexerFactory;
            this.hostLexer = hostLexer;
            this.portLexer = portLexer;
        }

        public ILexer<Host> Create()
        {
            var innerLexer = concatenationLexerFactory.Create(
                hostLexer,
                optionLexerFactory.Create(
                    concatenationLexerFactory.Create(
                        terminalLexerFactory.Create(@":", StringComparer.Ordinal),
                        portLexer)));
            return new HostLexer(innerLexer);
        }
    }
}
