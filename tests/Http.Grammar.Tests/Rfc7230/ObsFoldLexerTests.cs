using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    [TestClass]
    public class ObsFoldLexerTests
    {
        [TestMethod]
        public void ReadObsoleteFold1()
        {
            const string input = "\r\n ";
            var lexer = new ObsFoldLexer();
            using (ITextScanner scanner = new TextScanner(new StringReader(input)))
            {
                scanner.Read();
                var token = lexer.Read(scanner);
                Assert.IsNotNull(token);
                Assert.IsNotNull(token.Data);
                Assert.AreEqual(input, token.Data);
            }
        }

        [TestMethod]
        public void ReadObsoleteFold2()
        {
            const string input = "\r\n\t";
            var lexer = new ObsFoldLexer();
            using (ITextScanner scanner = new TextScanner(new StringReader(input)))
            {
                scanner.Read();
                var token = lexer.Read(scanner);
                Assert.IsNotNull(token);
                Assert.IsNotNull(token.Data);
                Assert.AreEqual(input, token.Data);
            }
        }

        [TestMethod]
        public void ReadObsoleteFold3()
        {
            const string input = "\r\n \t";
            var lexer = new ObsFoldLexer();
            using (ITextScanner scanner = new TextScanner(new StringReader(input)))
            {
                scanner.Read();
                var token = lexer.Read(scanner);
                Assert.IsNotNull(token);
                Assert.IsNotNull(token.Data);
                Assert.AreEqual(input, token.Data);
            }
        }

        [TestMethod]
        public void ReadObsoleteFold4()
        {
            const string input = "\r\n\t ";
            var lexer = new ObsFoldLexer();
            using (ITextScanner scanner = new TextScanner(new StringReader(input)))
            {
                scanner.Read();
                var token = lexer.Read(scanner);
                Assert.IsNotNull(token);
                Assert.IsNotNull(token.Data);
                Assert.AreEqual(input, token.Data);
            }
        }
    }
}
