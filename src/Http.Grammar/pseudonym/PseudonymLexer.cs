namespace Http.Grammar
{
    using System;

    using TextFx;

    public class PseudonymLexer : Lexer<Pseudonym>
    {
        private readonly ILexer<Token> innerLexer;

        public PseudonymLexer(ILexer<Token> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<Pseudonym> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return
                    ReadResult<Pseudonym>.FromError(
                        new SyntaxError
                        {
                            Message = "Expected 'pseudonym'.",
                            RuleName = "pseudonym",
                            Context = scanner.GetContext(),
                            InnerError = result.Error
                        });
            }

            var element = new Pseudonym(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<Pseudonym>.FromResult(element);
        }
    }
}