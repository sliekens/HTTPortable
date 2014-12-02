using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Http
{
    [TestClass]
    public class EntityHeaderCollectionForNewMessage
    {
        private HeaderCollection headers;

        [TestInitialize]
        public void Initialize()
        {
            this.headers = new EntityHeaderCollection();
        }

        [TestMethod]
        public void HeaderCollectionShouldContainAllow()
        {
            try
            {
                Assert.IsNotNull(this.headers["Allow"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainContentEncoding()
        {
            try
            {
                Assert.IsNotNull(this.headers["Content-Encoding"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainContentLanguage()
        {
            try
            {
                Assert.IsNotNull(this.headers["Content-Language"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainContentLength()
        {
            try
            {
                Assert.IsNotNull(this.headers["Content-Length"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainContentLocation()
        {
            try
            {
                Assert.IsNotNull(this.headers["Content-Location"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainContentMD5()
        {
            try
            {
                Assert.IsNotNull(this.headers["Content-MD5"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }
        [TestMethod]
        public void HeaderCollectionShouldContainContentRange()
        {
            try
            {
                Assert.IsNotNull(this.headers["Content-Range"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainContentType()
        {
            try
            {
                Assert.IsNotNull(this.headers["Content-Type"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainExpires()
        {
            try
            {
                Assert.IsNotNull(this.headers["Expires"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainLastModified()
        {
            try
            {
                Assert.IsNotNull(this.headers["Last-Modified"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }
    }
}
