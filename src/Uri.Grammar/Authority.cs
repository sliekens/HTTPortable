namespace Uri.Grammar
{
    using System.Diagnostics;
    using System.Diagnostics.Contracts;
    using SLANG;

    /// <summary>Represents a URI's naming authority.</summary>
    public class Authority : Element
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Host host;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Port port;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly UserInformation userInformation;

        /// <summary>Initializes a new instance of the <see cref="Authority"/> class.</summary>
        /// <param name="userInformation">The optional user information component.</param>
        /// <param name="userInformationSeparator">The optional user information separator. If the user information is specified, then the separator is required.</param>
        /// <param name="host">The host component.</param>
        /// <param name="portSeparator">The optional port separator.</param>
        /// <param name="port">The optional port. If the port separator is specified, then the port is required.</param>
        /// <param name="context">The object that describes the context in which the text appears.</param>
        public Authority(UserInformation userInformation, Element userInformationSeparator, Host host, 
            Element portSeparator, Port port, ITextContext context)
            : base(string.Concat(userInformation, userInformationSeparator, host, portSeparator, port), context)
        {
            this.userInformation = userInformation;
            this.host = host;
            this.port = port;
            Contract.Requires(userInformation == null ||
                              (userInformationSeparator != null && userInformationSeparator.Data == "@"));
            Contract.Requires(host != null);
            Contract.Requires(portSeparator == null || (portSeparator.Data == ":" && port != null));
            Contract.Requires(context != null);
        }

        /// <summary>Gets the host.</summary>
        public Host Host
        {
            get
            {
                return this.host;
            }
        }

        /// <summary>Gets the optional port.</summary>
        public Port Port
        {
            get
            {
                return this.port;
            }
        }

        /// <summary>Gets the optional user information.</summary>
        public UserInformation UserInformation
        {
            get
            {
                return this.userInformation;
            }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.host != null);
        }
    }
}