namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class SchemeLexer : Lexer<Scheme>
    {
        private readonly ILexer<Sequence> innerLexer;

        public SchemeLexer(ILexer<Sequence> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<Scheme> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<Scheme>.FromError(new SyntaxError
                {
                    Message = "Expected 'scheme'",
                    RuleName = "scheme",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new Scheme(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<Scheme>.FromResult(element);
        }
    }
}