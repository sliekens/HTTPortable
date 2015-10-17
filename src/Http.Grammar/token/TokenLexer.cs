namespace Http.Grammar
{
    using System;

    using TextFx;

    public class TokenLexer : Lexer<Token>
    {
        public override ReadResult<Token> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            throw new NotImplementedException();
        }
    }
}