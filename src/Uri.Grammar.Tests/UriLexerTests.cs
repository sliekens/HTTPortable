namespace Uri.Grammar
{
    using System.IO;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using SLANG;

    

    [TestClass]
    public class UriLexerTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "URI.xml", "Row", DataAccessMethod.Sequential)]
        public void ReadUri()
        {
            var dataRow = this.TestContext.DataRow;
            var input = (string)dataRow["Input"];
            var expected = (string)dataRow["Expected"];
            var lexer = new UriLexer();
            using (TextReader textReader = new StringReader(input))
            using (ITextScanner scanner = new TextScanner(textReader))
            {
                scanner.Read();
                var element = lexer.Read(scanner);
                Assert.IsNotNull(element);
                Assert.AreEqual(expected, element.Data);
            }
        }
    }
}
