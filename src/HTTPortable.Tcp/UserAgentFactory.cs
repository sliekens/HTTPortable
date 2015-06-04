namespace Http.Tcp
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;
    using System.Threading.Tasks;

    public class UserAgentFactory : IUserAgentFactory
    {
        /// <inheritdoc />
        public async Task<IUserAgent> CreateAsync(Uri uri, CancellationToken cancellationToken)
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var endpoint = new DnsEndPoint(uri.DnsSafeHost, uri.Port, AddressFamily.InterNetwork);
            var ar = socket.BeginConnect(endpoint, asyncResult => socket.EndConnect(asyncResult), null);
            await Task.Run(() => ar.AsyncWaitHandle.WaitOne(), cancellationToken);
            var stream = new NetworkStream(socket, FileAccess.ReadWrite, true);
            return new UserAgent(stream);
        }
    }
}