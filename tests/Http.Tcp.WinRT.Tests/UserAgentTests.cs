using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Windows.Web.Http;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Http.Tcp.WinRT.Tests
{
    [TestClass]
    public class WithHttpBin
    {
        private IUserAgent userAgent;

        [TestInitialize]
        public async Task TestInitialize()
        {
            var socket = new StreamSocket();
            await socket.ConnectAsync(new HostName("httpbin.org"), "80");
            this.userAgent = new UserAgent(socket);
        }

        public TestContext TestContext { get; set; }

        [TestMethod]
        public async Task GetOriginIp()
        {
            var request = new RequestMessage("GET", "/ip", Version.Parse("1.1"));
            request.Headers.Connection.Add("keep-alive");
            request.RequestHeaders.UserAgent.Add("UA");
            request.RequestHeaders.Host.Add("httpbin.org");
            request.RequestHeaders.Accept.Add("application/json");
            await userAgent.SendAsync(request, CancellationToken.None);
            var contentBuffer = new StringBuilder();
            await userAgent.ReceiveAsync(CancellationToken.None, async (message, stream, cancellationToken) =>
            {
                var length = Convert.ToInt32(message.ContentHeaders.ContentLength.FirstOrDefault());
                var buffer = new byte[length];
                await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
                contentBuffer.Append(Encoding.UTF8.GetString(buffer, 0, buffer.Length));
            });

            var content = contentBuffer.ToString();
            Debug.WriteLine(content);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.userAgent.Dispose();
        }
    }
}
