namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class UriReferenceLexer : Lexer<UriReference>
    {
        private readonly ILexer<Alternative> innerLexer;

        public UriReferenceLexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<UriReference> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<UriReference>.FromError(new SyntaxError
                {
                    Message = "Expected 'URI-reference'",
                    RuleName = "URI-reference",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new UriReference(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<UriReference>.FromResult(element);
        }
    }
}