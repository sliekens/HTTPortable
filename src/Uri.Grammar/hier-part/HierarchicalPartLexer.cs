namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class HierarchicalPartLexer : Lexer<HierarchicalPart>
    {
        private readonly ILexer<Alternative> innerLexer;

        public HierarchicalPartLexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<HierarchicalPart> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<HierarchicalPart>.FromError(new SyntaxError
                {
                    Message = "Expected 'fragment'.",
                    RuleName = "fragment",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new HierarchicalPart(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<HierarchicalPart>.FromResult(element);
        }
    }
}