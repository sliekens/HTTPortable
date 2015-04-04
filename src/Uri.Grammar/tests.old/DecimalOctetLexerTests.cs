namespace Uri.Grammar
{
    using System.IO;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using SLANG;



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
            using (var inputStream = new MemoryStream(Encoding.ASCII.GetBytes(input)))
            using (var pushbackInputStream = new PushbackInputStream(inputStream))
            using (ITextScanner textScanner = new TextScanner(pushbackInputStream))
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
