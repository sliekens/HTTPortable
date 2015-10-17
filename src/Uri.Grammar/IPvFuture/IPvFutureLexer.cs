namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class IPvFutureLexer : Lexer<IPvFuture>
    {
        private readonly ILexer<Sequence> innerLexer;

        public IPvFutureLexer(ILexer<Sequence> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<IPvFuture> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<IPvFuture>.FromError(new SyntaxError
                {
                    Message = "Expected 'IPvFuture'.",
                    RuleName = "IPvFuture",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new IPvFuture(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<IPvFuture>.FromResult(element);
        }
    }
}