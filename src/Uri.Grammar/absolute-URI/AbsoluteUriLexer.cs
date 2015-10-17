namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class AbsoluteUriLexer : Lexer<AbsoluteUri>
    {
        private readonly ILexer<Sequence> innerLexer;

        public AbsoluteUriLexer(ILexer<Sequence> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<AbsoluteUri> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<AbsoluteUri>.FromError(new SyntaxError
                {
                    Message = "Expected 'absolute-URI'.",
                    RuleName = "absolute-URI",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new AbsoluteUri(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<AbsoluteUri>.FromResult(element);
        }
    }
}