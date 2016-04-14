using System;
using Txt;

namespace Http.qdtext
{
    public class QuotedTextLexer : Lexer<QuotedText>
    {
        public override ReadResult<QuotedText> Read(ITextScanner scanner)
        {
            throw new NotImplementedException();
        }
    }
}