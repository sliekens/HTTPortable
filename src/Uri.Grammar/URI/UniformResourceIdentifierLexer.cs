namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class UniformResourceIdentifierLexer : Lexer<UniformResourceIdentifier>
    {
        private readonly ILexer<Sequence> innerLexer;

        public UniformResourceIdentifierLexer(ILexer<Sequence> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<UniformResourceIdentifier> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<UniformResourceIdentifier>.FromError(new SyntaxError
                {
                    Message = "Expected 'URI'",
                    RuleName = "URI",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new UniformResourceIdentifier(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<UniformResourceIdentifier>.FromResult(element);
        }
    }
}