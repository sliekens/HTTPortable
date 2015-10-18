namespace Http.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class TokenLexer : Lexer<Token>
    {
        private readonly ILexer<Repetition> innerLexer;

        public TokenLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<Token> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return
                    ReadResult<Token>.FromError(
                        new SyntaxError
                            {
                                Message = "Expected 'token'.",
                                RuleName = "token",
                                Context = scanner.GetContext(),
                                InnerError = result.Error
                            });
            }

            var element = new Token(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<Token>.FromResult(element);
        }
    }
}