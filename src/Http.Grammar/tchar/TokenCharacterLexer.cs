namespace Http.Grammar
{
    using System;

    using TextFx;

    public class TokenCharacterLexer : Lexer<TokenCharacter>
    {
        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out TokenCharacter element)
        {
            throw new NotImplementedException();
        }
    }
}