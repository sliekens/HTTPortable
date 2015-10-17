namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class LeastSignificantInt32Lexer : Lexer<LeastSignificantInt32>
    {
        private readonly ILexer<Alternative> innerLexer;

        public LeastSignificantInt32Lexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<LeastSignificantInt32> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<LeastSignificantInt32>.FromError(new SyntaxError
                {
                    Message = "Expected 'ls32'.",
                    RuleName = "ls32",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new LeastSignificantInt32(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<LeastSignificantInt32>.FromResult(element);
        }
    }
}