namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class RelativeReferenceLexer : Lexer<RelativeReference>
    {
        private readonly ILexer<Sequence> innerLexer;

        public RelativeReferenceLexer(ILexer<Sequence> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<RelativeReference> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<RelativeReference>.FromError(new SyntaxError
                {
                    Message = "Expected 'relative-ref'",
                    RuleName = "relative-ref",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new RelativeReference(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<RelativeReference>.FromResult(element);
        }
    }
}