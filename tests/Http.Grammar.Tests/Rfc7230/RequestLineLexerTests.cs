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
    public class RequestLineLexerTests
    {
        [TestMethod]
        public void ReadRequestLine1()
        {
            const string input = "GET /StevenLiekens/http-client HTTP/1.1\r\n";
            var lexer = new RequestLineLexer();
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
