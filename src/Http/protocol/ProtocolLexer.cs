using System;
using Txt;

namespace Http.protocol
{
    public class ProtocolLexer : Lexer<Protocol>
    {
        public override ReadResult<Protocol> Read(ITextScanner scanner)
        {
            throw new NotImplementedException();
        }
    }
}