using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Http;
using Http.HTTP_message;
using Txt.ABNF;
using Txt.Core;

namespace HTTPortable
{
    public class TcpUserAgentFactory : IUserAgentFactory
    {
        /// <inheritdoc />
        public async Task<IUserAgent> CreateAsync(Uri uri, CancellationToken cancellationToken)
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var endpoint = new DnsEndPoint(uri.DnsSafeHost, uri.Port, AddressFamily.InterNetwork);
            await socket.ConnectAsync(endpoint).ConfigureAwait(false);
            Stream stream = new NetworkStream(socket, true);
            if (string.Equals(uri.Scheme, "https", StringComparison.OrdinalIgnoreCase))
            {
                var sslStream = new SslStream(stream);
                await sslStream.AuthenticateAsClientAsync(uri.Host).ConfigureAwait(false);
                stream = sslStream;
            }
            return new UserAgent(stream, HttpMessageLexerFactory.Default.Create());
        }
    }
}
