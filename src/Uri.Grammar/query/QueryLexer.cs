namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class QueryLexer : Lexer<Query>
    {
        private readonly ILexer<Repetition> innerLexer;

        public QueryLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer", "Precondition: innerLexer != null");
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<Query> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<Query>.FromError(new SyntaxError
                {
                    Message = "Expected 'query'.",
                    RuleName = "query",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new Query(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<Query>.FromResult(element);
        }
    }
}