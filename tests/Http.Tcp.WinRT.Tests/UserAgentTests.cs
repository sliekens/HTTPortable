using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Data.Json;
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

        [DataContract]
        class MessageBody
        {
            [DataMember(Name = "origin")]
            public string Origin { get; set; }
        }

        [TestMethod]
        public async Task GetOriginIp()
        {
            MessageBody messageBody = null;
            var request = new RequestMessage("GET", "/ip", Version.Parse("1.1"));
            request.Headers.Add(new Header("Connection") { "keep-alive" });
            request.Headers.Add(new Header("User-Agent") { "UA" });
            request.Headers.Add(new Header("Host")       { "httpbin.org" });
            request.Headers.Add(new Header("Accept")     { "application/json" });
            await userAgent.SendAsync(request, CancellationToken.None);
            await userAgent.ReceiveAsync(CancellationToken.None, async (message, stream, cancellationToken) =>
            {
                messageBody = (MessageBody)new DataContractJsonSerializer(typeof(MessageBody)).ReadObject(stream);
            });

            Debug.WriteLine(messageBody.Origin);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.userAgent.Dispose();
        }
    }
}
