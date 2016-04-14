using System;
using Txt;

namespace Http.obs_text
{
    public class ObsoleteTextLexer : Lexer<ObsoleteText>
    {
        public override ReadResult<ObsoleteText> Read(ITextScanner scanner)
        {
            throw new NotImplementedException();
        }
    }
}