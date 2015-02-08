using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    [TestClass]
    public class StatusCodeLexerTests
    {
        [TestMethod]
        public void Read100()
        {
            var text = "100";
            using (var reader = new StringReader(text))
            using (ITextScanner scanner = new TextScanner(reader))
            {
                scanner.Read();
                var lexer = new StatusCodeLexer();
                var token = lexer.Read(scanner);
                Assert.AreEqual(text, token.Data);
            }
        }
    }
}
