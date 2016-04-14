using System;
using Txt;

namespace Http.header_field
{
    public class HeaderFieldLexer : Lexer<HeaderField>
    {
        public override ReadResult<HeaderField> Read(ITextScanner scanner)
        {
            throw new NotImplementedException();
        }
    }
}
