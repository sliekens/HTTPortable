using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Http.Grammar.Rfc7230
{
    using System.Text;

    using SLANG;

    [TestClass]
    public class TokenCharacterLexerTests
    {
        [TestMethod]
        public void ReadDigits()
        {
            var lexer = new TokenCharacterLexer();
            char[] input = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            foreach (var c in input)
            {
                var text = new string(c, 1);
                using (var inputStream = new MemoryStream(Encoding.ASCII.GetBytes(text)))
                using (var pushbackInputStream = new PushbackInputStream(inputStream))
                using (ITextScanner textScanner = new TextScanner(pushbackInputStream))
                {
                    textScanner.Read();
                    var output = lexer.Read(textScanner);
                    Assert.IsNotNull(output.Data);
                    Assert.AreEqual(text, output.Data);
                }
            }
        }

        [TestMethod]
        public void ReadAlphas()
        {
            var lexer = new TokenCharacterLexer();
            foreach (char c in Enumerable.Range('a', 26))
            {
                var text = new string(c, 1);
                using (var inputStream = new MemoryStream(Encoding.ASCII.GetBytes(text)))
                using (var pushbackInputStream = new PushbackInputStream(inputStream))
                using (ITextScanner textScanner = new TextScanner(pushbackInputStream))
                {
                    textScanner.Read();
                    var output = lexer.Read(textScanner);
                    Assert.IsNotNull(output.Data);
                    Assert.AreEqual(text, output.Data);
                }
            }
        }

        [TestMethod]
        public void ReadSymbols()
        {
            var lexer = new TokenCharacterLexer();
            foreach (char c in new[] { '!', '#', '$', '%', '&', '\'', '*', '+', '-', '.', '^', '_', '`', '|', '~' })
            {
                var text = new string(c, 1);
                using (var inputStream = new MemoryStream(Encoding.ASCII.GetBytes(text)))
                using (var pushbackInputStream = new PushbackInputStream(inputStream))
                using (ITextScanner textScanner = new TextScanner(pushbackInputStream))
                {
                    textScanner.Read();
                    var output = lexer.Read(textScanner);
                    Assert.IsNotNull(output.Data);
                    Assert.AreEqual(text, output.Data);
                }
            }
        }
    }
}
