using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Http
{
    [TestClass]
    public class GeneralHeaderCollectionForNewMessage
    {
        private HeaderCollection headers;

        [TestInitialize]
        public void Initialize()
        {
            this.headers = new GeneralHeaderCollection();
        }

        [TestMethod]
        public void HeaderCollectionShouldContainCacheControl()
        {
            try
            {
                Assert.IsNotNull(this.headers["Cache-Control"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainConnection()
        {
            try
            {
                Assert.IsNotNull(this.headers["Connection"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainDate()
        {
            try
            {
                Assert.IsNotNull(this.headers["Date"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainPragma()
        {
            try
            {
                Assert.IsNotNull(this.headers["Pragma"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainTrailer()
        {
            try
            {
                Assert.IsNotNull(this.headers["Trailer"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainTransferEncoding()
        {
            try
            {
                Assert.IsNotNull(this.headers["Transfer-Encoding"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainUpgrade()
        {
            try
            {
                Assert.IsNotNull(this.headers["Upgrade"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainVia()
        {
            try
            {
                Assert.IsNotNull(this.headers["Via"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod]
        public void HeaderCollectionShouldContainWarning()
        {
            try
            {
                Assert.IsNotNull(this.headers["Warning"]);
            }
            catch (KeyNotFoundException exception)
            {
                Assert.Fail(exception.Message);
            }
        }
    }
}
