namespace Http.Grammar
{
    using System;

    using TextFx;

    public class CommentTextLexer : Lexer<CommentText>
    {
        public override ReadResult<CommentText> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            throw new NotImplementedException();
        }
    }
}