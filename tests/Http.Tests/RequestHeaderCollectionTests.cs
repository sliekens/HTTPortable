using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Http
{
    [TestClass]
    public class RequestHeaderCollectionForNewRequestMessage
    {
        private HeaderCollection headers;

        [TestInitialize]
        public void Initialize()
        {
            this.headers = new RequestHeaderCollection();
        }

        [TestMethod]
        public void HeaderCollectionShouldContainAccept()
        {
            try
            {
                Assert.IsNotNull(this.headers["Accept"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainAcceptCharset()
        {
            try
            {
                Assert.IsNotNull(this.headers["Accept-Charset"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainAcceptEncoding()
        {
            try
            {
                Assert.IsNotNull(this.headers["Accept-Encoding"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainAcceptLanguage()
        {
            try
            {
                Assert.IsNotNull(this.headers["Accept-Language"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainAuthorization()
        {
            try
            {
                Assert.IsNotNull(this.headers["Authorization"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainExpect()
        {
            try
            {
                Assert.IsNotNull(this.headers["Expect"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainFrom()
        {
            try
            {
                Assert.IsNotNull(this.headers["From"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainHost()
        {
            try
            {
                Assert.IsNotNull(this.headers["Host"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainIfMatch()
        {
            try
            {
                Assert.IsNotNull(this.headers["If-Match"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainIfModifiedSince()
        {
            try
            {
                Assert.IsNotNull(this.headers["If-Modified-Since"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainIfNoneMatch()
        {
            try
            {
                Assert.IsNotNull(this.headers["If-None-Match"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainIfRange()
        {
            try
            {
                Assert.IsNotNull(this.headers["If-Range"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainIfUnmodifiedSince()
        {
            try
            {
                Assert.IsNotNull(this.headers["If-Unmodified-Since"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainMaxForwards()
        {
            try
            {
                Assert.IsNotNull(this.headers["Max-Forwards"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainProxyAuthorization()
        {
            try
            {
                Assert.IsNotNull(this.headers["Proxy-Authorization"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainRange()
        {
            try
            {
                Assert.IsNotNull(this.headers["Range"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainReferer()
        {
            try
            {
                Assert.IsNotNull(this.headers["Referer"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainTE()
        {
            try
            {
                Assert.IsNotNull(this.headers["TE"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainUserAgent()
        {
            try
            {
                Assert.IsNotNull(this.headers["User-Agent"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }
    }
}
