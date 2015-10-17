namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class RelativePartLexer : Lexer<RelativePart>
    {
        private readonly ILexer<Alternative> innerLexer;

        public RelativePartLexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<RelativePart> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<RelativePart>.FromError(new SyntaxError
                {
                    Message = "Expected 'relative-part'.",
                    RuleName = "relative-part",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new RelativePart(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<RelativePart>.FromResult(element);
        }
    }
}