namespace Http.Grammar
{
    using System;

    using TextFx;

    public class CommentTextLexer : Lexer<CommentText>
    {
        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out CommentText element)
        {
            throw new NotImplementedException();
        }
    }
}