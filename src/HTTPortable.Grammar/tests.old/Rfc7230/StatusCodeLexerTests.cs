using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Http.Grammar.Rfc7230
{
    using System.Text;

    using SLANG;

    [TestClass]
    public class StatusCodeLexerTests
    {
        [TestMethod]
        public void Read100()
        {
            var text = "100";
            using (var inputStream = new MemoryStream(Encoding.ASCII.GetBytes(text)))
            using (var pushbackInputStream = new PushbackInputStream(inputStream))
            using (ITextScanner scanner = new TextScanner(pushbackInputStream))
            {
                scanner.Read();
                var lexer = new StatusCodeLexer();
                var token = lexer.Read(scanner);
                Assert.AreEqual(text, token.Data);
            }
        }
    }
}
