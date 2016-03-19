using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using HTTPortable.Core;

namespace HTTPortable.Tcp
{
    public class UserAgentFactory : IUserAgentFactory
    {
        /// <inheritdoc />
        public async Task<IUserAgent> CreateAsync(Uri uri, CancellationToken cancellationToken)
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var endpoint = new DnsEndPoint(uri.DnsSafeHost, uri.Port, AddressFamily.InterNetwork);
            var ar = socket.BeginConnect(endpoint, asyncResult => socket.EndConnect(asyncResult), null);
            await Task.Run(() => ar.AsyncWaitHandle.WaitOne(), cancellationToken);
            Stream stream = new NetworkStream(socket, FileAccess.ReadWrite, true);
            if (string.Equals(uri.Scheme, "https", StringComparison.OrdinalIgnoreCase))
            {
                var sslStream = new SslStream(stream);
                await sslStream.AuthenticateAsClientAsync(uri.Host).ConfigureAwait(false);
                stream = sslStream;
            }

            return new UserAgent(stream);
        }
    }
}