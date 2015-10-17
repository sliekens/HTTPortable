namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class AuthorityLexer : Lexer<Authority>
    {
        private readonly ILexer<Sequence> innerLexer;

        public AuthorityLexer(ILexer<Sequence> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<Authority> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<Authority>.FromError(new SyntaxError
                {
                    Message = "Expected 'authority'.",
                    RuleName = "authority",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new Authority(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<Authority>.FromResult(element);
        }
    }
}