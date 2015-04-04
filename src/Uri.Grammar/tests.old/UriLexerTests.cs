namespace Uri.Grammar
{
    using System.IO;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using SLANG;

    

    [TestClass]
    public class UriLexerTests
    {
        public static MemoryStream ms { get; set; }
        public TestContext TestContext { get; set; }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "URI.xml", "Row", DataAccessMethod.Sequential)]
        public void ReadUri()
        {
            var dataRow = this.TestContext.DataRow;
            var input = (string)dataRow["Input"];
            var expected = (string)dataRow["Expected"];
            var lexer = new UriLexer();
            using (var inputStream = new MemoryStream(Encoding.ASCII.GetBytes(input)))
            using (var pushbackInputStream = new PushbackInputStream(inputStream))
            using (ITextScanner scanner = new TextScanner(pushbackInputStream))
            {
                ms = inputStream;
                scanner.Read();
                var element = lexer.Read(scanner);
                Assert.IsNotNull(element);
                Assert.AreEqual(expected, element.Data);
            }
        }
    }
}
