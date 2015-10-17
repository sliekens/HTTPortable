namespace Http.Grammar
{
    using System;

    using TextFx;

    public class TokenCharacterLexer : Lexer<TokenCharacter>
    {
        public override ReadResult<TokenCharacter> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            throw new NotImplementedException();
        }
    }
}