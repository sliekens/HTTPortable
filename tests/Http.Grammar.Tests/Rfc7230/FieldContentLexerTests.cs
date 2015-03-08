using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Http.Grammar.Rfc7230
{
    using SLANG;

    [TestClass]
    public class FieldContentLexerTests
    {
        [TestMethod]
        public void ReadFieldContent1()
        {
            const string input = "a";
            var lexer = new FieldContentLexer();
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
        public void ReadFieldContent2()
        {
            const string input = "a b";
            var lexer = new FieldContentLexer();
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
