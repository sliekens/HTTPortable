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
using Windows.Networking;
using Windows.Networking.Sockets;
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
        class OriginDataContract
        {
            [DataMember(Name = "origin")]
            public string Origin { get; set; }
        }

        [TestMethod]
        public async Task GetOriginIp()
        {
            OriginDataContract originDataContract = null;
            var request = new RequestMessage("GET", "/ip", Version.Parse("1.1"));
            request.Headers.Add(new Header("Connection") { "keep-alive" });
            request.Headers.Add(new Header("User-Agent") { "UA" });
            request.Headers.Add(new Header("Host") { "httpbin.org" });
            request.Headers.Add(new Header("Accept") { "application/json" });
            await userAgent.SendAsync(request, CancellationToken.None);
            await userAgent.ReceiveAsync(CancellationToken.None, (message, stream, cancellationToken) =>
            {
                return Task.Run(() =>
                {
                    originDataContract = (OriginDataContract)new DataContractJsonSerializer(typeof(OriginDataContract)).ReadObject(stream);
                }, cancellationToken);
            });

            Debug.WriteLine(originDataContract.Origin);
        }

        [DataContract]
        class UserAgentDataContract
        {
            [DataMember(Name = "user-agent")]
            public string UserAgent { get; set; }
        }

        [TestMethod]
        public async Task GetUserAgent()
        {
            UserAgentDataContract dataContract = null;
            var request = new RequestMessage("GET", "/user-agent", Version.Parse("1.1"));
            request.Headers.Add(new Header("Host")       { "httpbin.org" });
            request.Headers.Add(new Header("User-Agent") { "https://github.com/StevenLiekens/http-client" });
            await this.userAgent.SendAsync(request, CancellationToken.None);
            await this.userAgent.ReceiveAsync(CancellationToken.None, (message, stream, cancellationToken) =>
            {
                return Task.Run(() =>
                {
                    dataContract = (UserAgentDataContract)new DataContractJsonSerializer(typeof(UserAgentDataContract)).ReadObject(stream);
                }, cancellationToken);
            });

            Assert.AreEqual("https://github.com/StevenLiekens/http-client", dataContract.UserAgent);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.userAgent.Dispose();
        }
    }
}
