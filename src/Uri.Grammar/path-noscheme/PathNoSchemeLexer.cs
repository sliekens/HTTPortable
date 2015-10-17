namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class PathNoSchemeLexer : Lexer<PathNoScheme>
    {
        private readonly ILexer<Sequence> innerLexer;

        /// <summary>
        /// </summary>
        /// <param name="innerLexer">segment-nz-nc *( "/" segment )</param>
        public PathNoSchemeLexer(ILexer<Sequence> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<PathNoScheme> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<PathNoScheme>.FromError(new SyntaxError
                {
                    Message = "Expected 'path-noscheme'.",
                    RuleName = "path-noscheme",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new PathNoScheme(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<PathNoScheme>.FromResult(element);
        }
    }
}