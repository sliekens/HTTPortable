namespace Http.Tcp.WinRT.Tests.Grammars.Rfc7230
{
    using System.IO;
    using Http.Grammars.Rfc7230;
    using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
    using Text.Scanning;

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
