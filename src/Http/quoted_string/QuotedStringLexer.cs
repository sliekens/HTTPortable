using System;
using Txt;

namespace Http.quoted_string
{
    public class QuotedStringLexer : Lexer<QuotedString>
    {
        public override ReadResult<QuotedString> Read(ITextScanner scanner)
        {
            throw new NotImplementedException();
        }
    }
}