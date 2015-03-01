namespace Uri.Grammar
{
    using System.IO;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Text.Scanning;

    [TestClass]
    public class LeastSignificantInt32LexerTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "ls32.xml", "Row", DataAccessMethod.Sequential)]
        public void ReadLeastSignificantInt32()
        {
            var dataRow = this.TestContext.DataRow;
            var input = (string)dataRow["Input"];
            var expected = (string)dataRow["Expected"];
            var lexer = new LeastSignificantInt32Lexer();
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
