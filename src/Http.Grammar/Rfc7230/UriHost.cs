namespace Http.Grammar.Rfc7230
{
    using System.Diagnostics.Contracts;

    using Text.Scanning;

    using Uri.Grammar;

    public class UriHost : Element
    {
        private readonly Host host;

        public UriHost(Host host, ITextContext context)
            : base(host.Data, context)
        {
            Contract.Requires(host != null);
            Contract.Requires(context != null);
            this.host = host;
        }

        public Host Host
        {
            get
            {
                return this.host;
            }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.host != null);
        }
    }
}