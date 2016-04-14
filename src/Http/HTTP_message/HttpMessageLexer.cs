using System;
using Txt;

namespace Http.HTTP_message
{
    public class HttpMessageLexer : Lexer<HttpMessage>
    {
        public override ReadResult<HttpMessage> Read(ITextScanner scanner)
        {
            throw new NotImplementedException();
        }
    }
}