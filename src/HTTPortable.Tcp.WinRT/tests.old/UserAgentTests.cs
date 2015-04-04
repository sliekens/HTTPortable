using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace Http.Tcp.WinRT
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
            Debug.WriteLine(dataContract.UserAgent);
        }

        [DataContract]
        class EchoDataContract
        {
            [DataMember]
            public Args args { get; set; }
            [DataMember]
            public string data { get; set; }
            [DataMember]
            public Files files { get; set; }
            [DataMember]
            public Form form { get; set; }
            [DataMember]
            public Headers headers { get; set; }
            [DataMember]
            public object json { get; set; }
            [DataMember]
            public string origin { get; set; }
            [DataMember]
            public string url { get; set; }
        }

        [DataContract]
        public class Args
        {
        }

        [DataContract]
        public class Files
        {
        }

        [DataContract]
        public class Form
        {
        }

        [DataContract]
        public class Headers
        {
            [DataMember]
            public string ConnectTime { get; set; }
            [DataMember]
            public string Connection { get; set; }
            [DataMember]
            public string ContentLength { get; set; }
            [DataMember]
            public string Host { get; set; }
            [DataMember]
            public string TotalRouteTime { get; set; }
            [DataMember]
            public string UserAgent { get; set; }
            [DataMember]
            public string Via { get; set; }
            [DataMember]
            public string XRequestId { get; set; }
        }


        [TestMethod]
        public async Task EchoPostData()
        {
            EchoDataContract dataContract = null;
            var content = "Echo";
            var request = new RequestMessage("POST", "http://httpbin.org/post", Version.Parse("1.1"));
            request.Headers.Add(new Header("Host") { "httpbin.org" });
            request.Headers.Add(new Header("User-Agent") { "https://github.com/StevenLiekens/http-client" });
            request.Headers.Add(new Header("Content-Length") { content.Length.ToString(NumberFormatInfo.InvariantInfo) });
            await this.userAgent.SendAsync(request, CancellationToken.None, (message, stream, token) =>
            {
                var bytes = Encoding.UTF8.GetBytes(content);
                return stream.WriteAsync(bytes, 0, bytes.Length, token);
            });

            await this.userAgent.ReceiveAsync(CancellationToken.None, async (message, stream, cancellationToken) =>
            {
                dataContract = (EchoDataContract)new DataContractJsonSerializer(typeof(EchoDataContract)).ReadObject(stream);
            });

            Assert.AreEqual(content, dataContract.data);
        }

        [TestMethod]
        public async Task StreamChunked()
        {
            var request = new RequestMessage("GET", "http://httpbin.org/stream/100", Version.Parse("1.1"));
            request.Headers.Add(new Header("Host") { "httpbin.org" });
            request.Headers.Add(new Header("User-Agent") { "https://github.com/StevenLiekens/http-client" });
            await this.userAgent.SendAsync(request, CancellationToken.None);
            await this.userAgent.ReceiveAsync(CancellationToken.None);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.userAgent.Dispose();
        }
    }
}
