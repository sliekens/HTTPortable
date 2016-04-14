using System;
using Txt;

namespace Http.status_line
{
    public class StatusLineLexer : Lexer<StatusLine>
    {
        public override ReadResult<StatusLine> Read(ITextScanner scanner)
        {
            throw new NotImplementedException();
        }
    }
}