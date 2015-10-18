namespace Http.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class TokenCharacterLexer : Lexer<TokenCharacter>
    {
        private readonly ILexer<Alternative> innerLexer;

        public TokenCharacterLexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<TokenCharacter> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<TokenCharacter>.FromError(new SyntaxError
                {
                    Message = "Expected 'tchar'.",
                    RuleName = "tchar",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new TokenCharacter(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<TokenCharacter>.FromResult(element);
        }
    }
}