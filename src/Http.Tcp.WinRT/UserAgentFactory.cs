using System;
using System.Threading.Tasks;

namespace Http.Tcp.WinRT
{
    using System.Globalization;
    using System.Threading;
    using Windows.Networking;
    using Windows.Networking.Sockets;

    /// <summary>
    /// A user agent factory that creates 
    /// </summary>
    public class UserAgentFactory : IUserAgentFactory
    {
        /// <inheritdoc />
        public async Task<IUserAgent> CreateAsync(Uri uri, CancellationToken cancellationToken)
        {
            var socket = new StreamSocket();
            var hostName = new HostName(uri.DnsSafeHost);
            var asyncAction = socket.ConnectAsync(hostName, uri.Port.ToString(NumberFormatInfo.InvariantInfo));
            cancellationToken.Register(asyncAction.Cancel);
            await asyncAction;
            return new UserAgent(socket);
        }
    }
}
