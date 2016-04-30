using System;
using Http.pseudonym;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Uri.port;

namespace Http.received_by
{
    public class ReceivedByLexerFactory : ILexerFactory<ReceivedBy>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexer<Uri.host.Host> hostLexer;

        private readonly IOptionLexerFactory optionLexerFactory;

        private readonly ILexer<Port> portLexer;

        private readonly ILexer<Pseudonym> pseudonymLexer;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public ReceivedByLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IOptionLexerFactory optionLexerFactory,
            [NotNull] ILexer<Uri.host.Host> hostLexer,
            [NotNull] ILexer<Port> portLexer,
            [NotNull] ILexer<Pseudonym> pseudonymLexer)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
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
            if (pseudonymLexer == null)
            {
                throw new ArgumentNullException(nameof(pseudonymLexer));
            }
            this.terminalLexerFactory = terminalLexerFactory;
            this.alternationLexerFactory = alternationLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.optionLexerFactory = optionLexerFactory;
            this.hostLexer = hostLexer;
            this.portLexer = portLexer;
            this.pseudonymLexer = pseudonymLexer;
        }

        public ILexer<ReceivedBy> Create()
        {
            return
                new ReceivedByLexer(
                    alternationLexerFactory.Create(
                        concatenationLexerFactory.Create(
                            hostLexer,
                            optionLexerFactory.Create(
                                concatenationLexerFactory.Create(
                                    terminalLexerFactory.Create(@":", StringComparer.Ordinal),
                                    portLexer))),
                        pseudonymLexer));
        }
    }
}
