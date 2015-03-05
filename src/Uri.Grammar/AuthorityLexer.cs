namespace Uri.Grammar
{
    using System.Diagnostics.Contracts;

    using Text.Scanning;
    using UserInfo = Text.Scanning.Sequence<UserInformation, Text.Scanning.Element>;
    using PortInfo = Text.Scanning.Sequence<Text.Scanning.Element, Port>;

    public class AuthorityLexer : Lexer<Authority>
    {
        private readonly ILexer<UserInformation> userInformationLexer;

        private readonly ILexer<Host> hostLexer;

        private readonly ILexer<Port> portLexer;

        public AuthorityLexer(ILexer<UserInformation> userInformationLexer, ILexer<Host> hostLexer, ILexer<Port> portLexer)
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

            var context = scanner.GetContext();
            UserInfo userInfo;
            var hasUserInfo = this.TryReadOptionalUserInformation(scanner, out userInfo);

            Host host;
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
            this.TryReadOptionalPort(scanner, out portInfo);

            element = new Authority(string.Concat(userInfo, host, portInfo), context);
            return true;
        }

        private bool TryReadCommercialAt(ITextScanner scanner, out Element element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Element);
                return false;
            }

            var context = scanner.GetContext();
            if (scanner.TryMatch('@'))
            {
                element = new Element("@", context);
                return true;
            }

            element = default(Element);
            return false;
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

            Element commercialAt;
            if (!this.TryReadCommercialAt(scanner, out commercialAt))
            {
                scanner.PutBack(userInformation.Data);
                element = default(UserInfo);
                return false;
            }

            element = new UserInfo(userInformation, commercialAt, context);
            return true;
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
            if (!this.TryReadColon(scanner, out colon))
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

        private bool TryReadColon(ITextScanner scanner, out Element element)
        {
            if (scanner.EndOfInput)
            {
                element = default(Element);
                return false;
            }

            var context = scanner.GetContext();
            if (scanner.TryMatch(':'))
            {
                element = new Element(":", context);
                return true;
            }

            element = default(Element);
            return false;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(false);
            Contract.Invariant(this.userInformationLexer != null);
            Contract.Invariant(this.hostLexer != null);
            Contract.Invariant(this.portLexer != null);
        }
    }
}