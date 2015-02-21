using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    [TestClass]
    public class OptionalWhiteSpaceLexerTests
    {
        [TestMethod]
        public void ReadWhiteSpaces()
        {
            const string input = " \t \t\t\t   ";
            var lexer = new OptionalWhiteSpaceLexer();
            using (TextReader textReader = new StringReader(input))
            using (ITextScanner textScanner = new TextScanner(textReader))
            {
                textScanner.Read();
                var token = lexer.Read(textScanner);
                Assert.IsNotNull(token);
                Assert.IsNotNull(token.Data);
                Assert.AreEqual(input, token.Data);
            }
        }
    }
}
