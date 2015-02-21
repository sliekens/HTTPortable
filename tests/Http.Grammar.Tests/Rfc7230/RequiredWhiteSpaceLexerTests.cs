using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    [TestClass]
    public class RequiredWhiteSpaceLexerTests
    {
        [TestMethod]
        public void ReadRequiredWhiteSpace1()
        {
            const string input = " ";
            var lexer = new RequiredWhiteSpaceLexer();
            using (ITextScanner textScanner = new TextScanner(new StringReader(input)))
            {
                textScanner.Read();
                var token = lexer.Read(textScanner);
                Assert.IsNotNull(token);
                Assert.IsNotNull(token.Data);
                Assert.AreEqual(input, token.Data);
            }
        }

        [TestMethod]
        public void ReadRequiredWhiteSpace2()
        {
            const string input = "\t";
            var lexer = new RequiredWhiteSpaceLexer();
            using (ITextScanner textScanner = new TextScanner(new StringReader(input)))
            {
                textScanner.Read();
                var token = lexer.Read(textScanner);
                Assert.IsNotNull(token);
                Assert.IsNotNull(token.Data);
                Assert.AreEqual(input, token.Data);
            }
        }

        [TestMethod]
        public void ReadRequiredWhiteSpace3()
        {
            const string input = " \t";
            var lexer = new RequiredWhiteSpaceLexer();
            using (ITextScanner textScanner = new TextScanner(new StringReader(input)))
            {
                textScanner.Read();
                var token = lexer.Read(textScanner);
                Assert.IsNotNull(token);
                Assert.IsNotNull(token.Data);
                Assert.AreEqual(input, token.Data);
            }
        }

        [TestMethod]
        public void ReadRequiredWhiteSpace4()
        {
            const string input = "\t ";
            var lexer = new RequiredWhiteSpaceLexer();
            using (ITextScanner textScanner = new TextScanner(new StringReader(input)))
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
