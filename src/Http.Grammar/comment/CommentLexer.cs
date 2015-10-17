namespace Http.Grammar
{
    using System;

    using TextFx;

    public class CommentLexer : Lexer<Comment>
    {
        public override ReadResult<Comment> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            throw new NotImplementedException();
        }
    }
}