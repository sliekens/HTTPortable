using System;
using Txt;

namespace Http.ctext
{
    public class CommentTextLexer : Lexer<CommentText>
    {
        public override ReadResult<CommentText> Read(ITextScanner scanner)
        {
            throw new NotImplementedException();
        }
    }
}