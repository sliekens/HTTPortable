using Http;

namespace HTTPortable.Tcp.Tests
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class IntegrationTests
    {
        [Fact]
        public async void GetGitHubPage()
        {
            IUserAgentFactory factory = new TcpUserAgentFactory();
            var uri = new Uri("https://github.com/");
            var userAgent = await factory.CreateAsync(uri, CancellationToken.None);
            using (userAgent)
            {
                var request = new RequestMessage("GET", "/StevenLiekens/HTTPortable", Version.Parse("1.1"));
                await userAgent.SendAsync(request, CancellationToken.None);
                await userAgent.ReceiveAsync(CancellationToken.None, Callback);
            }
        }

        private Task Callback(IResponseMessage responseMessage, Stream responseStream, CancellationToken cancellationToken)
        {
            Assert.Equal(200, responseMessage.Status);
            return Task.FromResult<object>(null);
        }
    }
}