using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Http
{
    [TestClass]
    public class ResponseHeaderCollectionForNewResponseMessage
    {
        private HeaderCollection headers;

        [TestInitialize]
        public void Initialize()
        {
            this.headers = new ResponseHeaderCollection();
        }

        [TestMethod]
        public void HeaderCollectionShouldContainAcceptRanges()
        {
            try
            {
                Assert.IsNotNull(this.headers["Accept-Ranges"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainAge()
        {
            try
            {
                Assert.IsNotNull(this.headers["Age"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainETag()
        {
            try
            {
                Assert.IsNotNull(this.headers["ETag"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainLocation()
        {
            try
            {
                Assert.IsNotNull(this.headers["Location"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainProxyAuthenticate()
        {
            try
            {
                Assert.IsNotNull(this.headers["Proxy-Authenticate"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainRetryAfter()
        {
            try
            {
                Assert.IsNotNull(this.headers["Retry-After"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainServer()
        {
            try
            {
                Assert.IsNotNull(this.headers["Server"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainVary()
        {
            try
            {
                Assert.IsNotNull(this.headers["Vary"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainWwwAuthenticate()
        {
            try
            {
                Assert.IsNotNull(this.headers["WWW-Authenticate"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }
    }
}
