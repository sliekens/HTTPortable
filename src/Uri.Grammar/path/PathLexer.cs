namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class PathLexer : Lexer<Path>
    {
        private readonly ILexer<Alternative> innerLexer;

        public PathLexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<Path> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<Path>.FromError(new SyntaxError
                {
                    Message = "Expected 'path'.",
                    RuleName = "path",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new Path(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<Path>.FromResult(element);
        }
    }
}