using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Text.Scanning;

namespace Http.Grammar.Rfc7230
{
    [TestClass]
    public class FieldValueLexerTests
    {
        [TestMethod]
        public void ReadFieldValue()
        {
            const string input = "application/x-javascript; charset=utf-8";
            var lexer = new FieldValueLexer();
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
