namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class AuthorityLexerFactory : ILexerFactory<Authority>
    {
        private readonly ILexerFactory<Host> hostLexerFactory;

        private readonly IOptionLexerFactory optionLexerFactory;

        private readonly ILexerFactory<Port> portLexerFactory;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        private readonly ILexerFactory<UserInformation> userInformationLexerFactory;

        public AuthorityLexerFactory(
            IOptionLexerFactory optionLexerFactory,
            IConcatenationLexerFactory concatenationLexerFactory,
            ILexerFactory<UserInformation> userInformationLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            ILexerFactory<Host> hostLexerFactory,
            ILexerFactory<Port> portLexerFactory)
        {
            if (optionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(optionLexerFactory));
            }

            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }

            if (userInformationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(userInformationLexerFactory));
            }

            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }

            if (hostLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(hostLexerFactory));
            }

            if (portLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(portLexerFactory));
            }

            this.optionLexerFactory = optionLexerFactory;
            this.userInformationLexerFactory = userInformationLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.hostLexerFactory = hostLexerFactory;
            this.portLexerFactory = portLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
        }

        public ILexer<Authority> Create()
        {
            var userinfo = userInformationLexerFactory.Create();
            var at = terminalLexerFactory.Create(@"@", StringComparer.Ordinal);
            var userinfoseq = concatenationLexerFactory.Create(userinfo, at);
            var optuserinfo = optionLexerFactory.Create(userinfoseq);
            var host = hostLexerFactory.Create();
            var colon = terminalLexerFactory.Create(@":", StringComparer.Ordinal);
            var port = portLexerFactory.Create();
            var portseq = concatenationLexerFactory.Create(colon, port);
            var optport = optionLexerFactory.Create(portseq);
            var innerLexer = concatenationLexerFactory.Create(optuserinfo, host, optport);
            return new AuthorityLexer(innerLexer);
        }
    }
}