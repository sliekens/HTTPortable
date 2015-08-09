namespace Http.Grammar
{
    using System;

    using TextFx;

    public class CommentLexer : Lexer<Comment>
    {
        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out Comment element)
        {
            throw new NotImplementedException();
        }
    }
}