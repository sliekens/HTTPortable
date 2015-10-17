namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class PathRootlessLexer : Lexer<PathRootless>
    {
        private readonly ILexer<Sequence> innerLexer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="innerLexer">segment-nz *( "/" segment )</param>
        public PathRootlessLexer(ILexer<Sequence> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<PathRootless> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<PathRootless>.FromError(new SyntaxError
                {
                    Message = "Expected 'path-rootless'.",
                    RuleName = "path-rootless",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new PathRootless(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<PathRootless>.FromResult(element);
        }
    }
}