namespace Uri.Grammar.authority
{
    using System;

    using SLANG;

    public class AuthorityLexerFactory : ILexerFactory<Authority>
    {
        private readonly ILexerFactory<Host> hostLexerFactory;

        private readonly IOptionLexerFactory optionLexerFactory;

        private readonly ILexerFactory<Port> portLexerFactory;

        private readonly ISequenceLexerFactory sequenceLexerFactory;

        private readonly IStringLexerFactory stringLexerFactory;

        private readonly ILexerFactory<UserInformation> userInformationLexerFactory;

        public AuthorityLexerFactory(
            IOptionLexerFactory optionLexerFactory,
            ISequenceLexerFactory sequenceLexerFactory,
            ILexerFactory<UserInformation> userInformationLexerFactory,
            IStringLexerFactory stringLexerFactory,
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

            if (stringLexerFactory == null)
            {
                throw new ArgumentNullException("stringLexerFactory");
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
            this.stringLexerFactory = stringLexerFactory;
            this.hostLexerFactory = hostLexerFactory;
            this.portLexerFactory = portLexerFactory;
            this.sequenceLexerFactory = sequenceLexerFactory;
        }

        public ILexer<Authority> Create()
        {
            var userinfo = this.userInformationLexerFactory.Create();
            var at = this.stringLexerFactory.Create(@"@");
            var userinfoseq = this.sequenceLexerFactory.Create(userinfo, at);
            var optuserinfo = this.optionLexerFactory.Create(userinfoseq);
            var host = this.hostLexerFactory.Create();
            var colon = this.stringLexerFactory.Create(@":");
            var port = this.portLexerFactory.Create();
            var portseq = this.sequenceLexerFactory.Create(colon, port);
            var optport = this.optionLexerFactory.Create(portseq);
            var innerLexer = this.sequenceLexerFactory.Create(optuserinfo, host, optport);
            return new AuthorityLexer(innerLexer);
        }
    }
}