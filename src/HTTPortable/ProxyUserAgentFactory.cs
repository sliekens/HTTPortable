using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Http;

namespace HTTPortable
{
    /// <summary>A user agent factory class that creates and configures user agents for a proxy server.</summary>
    public class ProxyUserAgentFactory : IUserAgentFactory
    {
        private readonly IUserAgentFactory userAgentFactory;

        private readonly IWebProxy webProxy;

        /// <summary>Creates a new instance of the <see cref="ProxyUserAgentFactory" /> class using the specified web proxy.</summary>
        /// <param name="userAgentFactory">The inner user agent factory that creates user agents for the final destination URI.</param>
        /// <param name="webProxy">The web proxy to connect to.</param>
        public ProxyUserAgentFactory(IUserAgentFactory userAgentFactory, IWebProxy webProxy)
        {
            if (userAgentFactory == null)
            {
                throw new ArgumentNullException(nameof(userAgentFactory));
            }
            if (webProxy == null)
            {
                throw new ArgumentNullException(nameof(webProxy));
            }
            this.userAgentFactory = userAgentFactory;
            this.webProxy = webProxy;
        }

        /// <inheritdoc />
        public Task<IUserAgent> CreateAsync(System.Uri uri, CancellationToken cancellationToken)
        {
            var proxy = webProxy.GetProxy(uri);
            return userAgentFactory.CreateAsync(proxy, cancellationToken);
        }
    }
}
