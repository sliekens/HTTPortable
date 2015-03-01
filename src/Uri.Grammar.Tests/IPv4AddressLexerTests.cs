using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Uri.Grammar
{
    using System.IO;

    using Text.Scanning;

    [TestClass]
    public class IPv4AddressLexerTests
    {
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "IPv4address.xml", "Row", DataAccessMethod.Sequential)]
        public void ReadIPv4Address()
        {
            var dataRow = this.TestContext.DataRow;
            var input = (string)dataRow["Input"];
            var expected = (string)dataRow["Expected"];
            var lexer = new IPv4AddressLexer();
            using (TextReader textReader = new StringReader(input))
            using (ITextScanner scanner = new TextScanner(textReader))
            {
                scanner.Read();
                var element = lexer.Read(scanner);
                Assert.IsNotNull(element);
                Assert.AreEqual(expected, element.Data);
            }
        }

        public TestContext TestContext { get; set; }
    }
}
