namespace Uri.Grammar
{
    using System.Diagnostics.Contracts;
    using SLANG;
    using PortInfo = SLANG.Sequence<SLANG.Element, Port>;
    using UserInfo = SLANG.Sequence<UserInformation, SLANG.Element>;

    public class AuthorityLexer : Lexer<Authority>
    {
        private readonly ILexer<Host> hostLexer;
        private readonly ILexer<Port> portLexer;
        private readonly ILexer<UserInformation> userInformationLexer;

        public AuthorityLexer()
            : this(new UserInformationLexer(), new HostLexer(), new PortLexer())
        {
        }

        public AuthorityLexer(ILexer<UserInformation> userInformationLexer, ILexer<Host> hostLexer, 
            ILexer<Port> portLexer)
            : base("authority")
        {
            Contract.Requires(userInformationLexer != null);
            Contract.Requires(hostLexer != null);
            Contract.Requires(portLexer != null);
            this.userInformationLexer = userInformationLexer;
            this.hostLexer = hostLexer;
            this.portLexer = portLexer;
        }

        public override bool TryRead(ITextScanner scanner, out Authority element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Authority);
                return false;
            }

            UserInformation userInformation = default(UserInformation);
            Element userInformationSeparator = default(Element);
            Host host;
            Element portSeparator = default(Element);
            Port port = default(Port);
            var context = scanner.GetContext();
            UserInfo userInfo;
            var hasUserInfo = this.TryReadOptionalUserInformation(scanner, out userInfo);
            if (hasUserInfo)
            {
                userInformation = userInfo.Element1;
                userInformationSeparator = userInfo.Element2;
            }

            if (!this.hostLexer.TryRead(scanner, out host))
            {
                if (hasUserInfo)
                {
                    scanner.PutBack(userInfo.Data);
                }

                element = default(Authority);
                return false;
            }

            PortInfo portInfo;
            if (this.TryReadOptionalPort(scanner, out portInfo))
            {
                portSeparator = portInfo.Element1;
                port = portInfo.Element2;
            }

            element = new Authority(userInformation, userInformationSeparator, host, portSeparator, port, context);
            return true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(false);
            Contract.Invariant(this.userInformationLexer != null);
            Contract.Invariant(this.hostLexer != null);
            Contract.Invariant(this.portLexer != null);
        }

        private bool TryReadOptionalPort(ITextScanner scanner, out PortInfo element)
        {
            if (scanner.EndOfInput)
            {
                element = default(PortInfo);
                return false;
            }

            var context = scanner.GetContext();
            Element colon;
            if (!TryReadTerminal(scanner, ':', out colon))
            {
                element = default(PortInfo);
                return false;
            }

            Port port;
            if (!this.portLexer.TryRead(scanner, out port))
            {
                scanner.PutBack(colon.Data);
                element = default(PortInfo);
                return false;
            }

            element = new PortInfo(colon, port, context);
            return true;
        }

        private bool TryReadOptionalUserInformation(ITextScanner scanner, out UserInfo element)
        {
            if (scanner.EndOfInput)
            {
                element = default(UserInfo);
                return false;
            }

            var context = scanner.GetContext();
            UserInformation userInformation;
            if (!this.userInformationLexer.TryRead(scanner, out userInformation))
            {
                element = default(UserInfo);
                return false;
            }

            Element at;
            if (!TryReadTerminal(scanner, '@', out at))
            {
                scanner.PutBack(userInformation.Data);
                element = default(UserInfo);
                return false;
            }

            element = new UserInfo(userInformation, at, context);
            return true;
        }
    }
}