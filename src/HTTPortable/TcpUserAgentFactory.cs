using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using SimpleInjector;
using Txt.ABNF;
using Txt.Core;
using UriSyntax;
using Http;
using Registration = Txt.Core.Registration;

namespace HTTPortable
{
    public class TcpUserAgentFactory : IUserAgentFactory
    {
        private readonly Container container = new Container();

        public TcpUserAgentFactory()
        {
            List<Registration> registrations = new List<Registration>();
            registrations.AddRange(AbnfRegistrations.GetRegistrations(container.GetInstance));
            registrations.AddRange(UriRegistrations.GetRegistrations(container.GetInstance));
            registrations.AddRange(HttpRegistrations.GetRegistrations(container.GetInstance));
            foreach (var registration in registrations)
            {
                if (registration.Implementation != null)
                {
                    container.RegisterSingleton(registration.Service, registration.Implementation);
                }
                if (registration.Instance != null)
                {
                    container.RegisterSingleton(registration.Service, registration.Instance);
                }
                if (registration.Factory != null)
                {
                    container.RegisterSingleton(registration.Service, registration.Factory);
                }
            }
        }

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
            return new UserAgent(stream, container.GetInstance<ILexer<Http.HTTP_message.HttpMessage>>());
        }
    }
}
