using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Http.Grammar.Rfc7230
{
    using SLANG;

    [TestClass]
    public class ObsoletedFoldLexerTests
    {
        [TestMethod]
        public void ReadObsoleteFold1()
        {
            const string input = "\r\n ";
            var lexer = new ObsoletedFoldLexer();
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
            var lexer = new ObsoletedFoldLexer();
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
            var lexer = new ObsoletedFoldLexer();
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
            var lexer = new ObsoletedFoldLexer();
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
