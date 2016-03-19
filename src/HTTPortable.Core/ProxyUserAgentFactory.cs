using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace HTTPortable.Core
{
    /// <summary>A user agent factory class that creates and configures user agents for a proxy server.</summary>
    public class ProxyUserAgentFactory : IUserAgentFactory
    {
        private readonly IUserAgentFactory userAgentFactory;
        private readonly IWebProxy webProxy;

        /// <summary>
        /// Creates a new instance of the <see cref="ProxyUserAgentFactory" /> class using the default web proxy. This
        /// constructor reads proxy settings from the app.config file. If there is no config file, the current user's Internet
        /// Explorer (IE) proxy settings are used.
        /// </summary>
        /// <param name="userAgentFactory">The inner user agent factory that creates user agents for the final destination URI.</param>
        public ProxyUserAgentFactory(IUserAgentFactory userAgentFactory)
            : this(userAgentFactory, WebRequest.DefaultWebProxy)
        {
        }

        /// <summary>Creates a new instance of the <see cref="ProxyUserAgentFactory" /> class using the specified web proxy.</summary>
        /// <param name="userAgentFactory">The inner user agent factory that creates user agents for the final destination URI.</param>
        /// <param name="webProxy">The web proxy to connect to.</param>
        public ProxyUserAgentFactory(IUserAgentFactory userAgentFactory, IWebProxy webProxy)
        {
            this.userAgentFactory = userAgentFactory;
            this.webProxy = webProxy;
        }

        /// <inheritdoc />
        public Task<IUserAgent> CreateAsync(Uri uri, CancellationToken cancellationToken)
        {
            return userAgentFactory.CreateAsync(webProxy.GetProxy(uri), cancellationToken);
        }
    }
}