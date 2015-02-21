using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    [TestClass]
    public class TCharLexerTests
    {
        [TestMethod]
        public void ReadDigits()
        {
            var lexer = new TCharLexer();
            char[] input = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            foreach (var c in input)
            {
                var text = new string(c, 1);
                using (TextReader textReader = new StringReader(text))
                using (ITextScanner textScanner = new TextScanner(textReader))
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
            var lexer = new TCharLexer();
            foreach (char c in Enumerable.Range('a', 26))
            {
                var text = new string(c, 1);
                using (TextReader textReader = new StringReader(text))
                using (ITextScanner textScanner = new TextScanner(textReader))
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
            var lexer = new TCharLexer();
            foreach (char c in new[] { '!', '#', '$', '%', '&', '\'', '*', '+', '-', '.', '^', '_', '`', '|', '~' })
            {
                var text = new string(c, 1);
                using (TextReader textReader = new StringReader(text))
                using (ITextScanner textScanner = new TextScanner(textReader))
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
