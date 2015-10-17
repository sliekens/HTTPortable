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

        private readonly ISequenceLexerFactory sequenceLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        private readonly ILexerFactory<UserInformation> userInformationLexerFactory;

        public AuthorityLexerFactory(
            IOptionLexerFactory optionLexerFactory,
            ISequenceLexerFactory sequenceLexerFactory,
            ILexerFactory<UserInformation> userInformationLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            ILexerFactory<Host> hostLexerFactory,
            ILexerFactory<Port> portLexerFactory)
        {
            if (optionLexerFactory == null)
            {
                throw new ArgumentNullException("optionLexerFactory");
            }

            if (sequenceLexerFactory == null)
            {
                throw new ArgumentNullException("sequenceLexerFactory");
            }

            if (userInformationLexerFactory == null)
            {
                throw new ArgumentNullException("userInformationLexerFactory");
            }

            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException("terminalLexerFactory");
            }

            if (hostLexerFactory == null)
            {
                throw new ArgumentNullException("hostLexerFactory");
            }

            if (portLexerFactory == null)
            {
                throw new ArgumentNullException("portLexerFactory");
            }

            this.optionLexerFactory = optionLexerFactory;
            this.userInformationLexerFactory = userInformationLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.hostLexerFactory = hostLexerFactory;
            this.portLexerFactory = portLexerFactory;
            this.sequenceLexerFactory = sequenceLexerFactory;
        }

        public ILexer<Authority> Create()
        {
            var userinfo = this.userInformationLexerFactory.Create();
            var at = this.terminalLexerFactory.Create(@"@", StringComparer.Ordinal);
            var userinfoseq = this.sequenceLexerFactory.Create(userinfo, at);
            var optuserinfo = this.optionLexerFactory.Create(userinfoseq);
            var host = this.hostLexerFactory.Create();
            var colon = this.terminalLexerFactory.Create(@":", StringComparer.Ordinal);
            var port = this.portLexerFactory.Create();
            var portseq = this.sequenceLexerFactory.Create(colon, port);
            var optport = this.optionLexerFactory.Create(portseq);
            var innerLexer = this.sequenceLexerFactory.Create(optuserinfo, host, optport);
            return new AuthorityLexer(innerLexer);
        }
    }
}