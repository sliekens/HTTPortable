namespace Uri.Grammar
{
    using System.IO;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Text.Scanning;

    [TestClass]
    public class DecimalOctetLexerTests
    {
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "dec-octet.xml", "Row", DataAccessMethod.Sequential)]
        public void ReadDecimalOctet()
        {
            var row = TestContext.DataRow;
            var input = (string)row["Input"];
            var expected = (string)row["Expected"];
            using (TextReader textReader = new StringReader(input))
            using (ITextScanner textScanner = new TextScanner(textReader))
            {
                textScanner.Read();
                var lexer = new DecimalOctetLexer();
                var element = lexer.Read(textScanner);
                Assert.IsNotNull(element);
                Assert.AreEqual(expected, element.Data);
            }
        }

        public TestContext TestContext { get; set; }
    }
}
