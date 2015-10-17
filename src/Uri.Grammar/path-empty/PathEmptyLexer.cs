namespace Uri.Grammar
{
    using System;

    using TextFx;

    public class PathEmptyLexer : Lexer<PathEmpty>
    {
        private readonly ILexer<Terminal> innerLexer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="innerLexer">""</param>
        public PathEmptyLexer(ILexer<Terminal> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<PathEmpty> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<PathEmpty>.FromError(new SyntaxError
                {
                    Message = "Expected 'path-empty'.",
                    RuleName = "path-empty",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new PathEmpty(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<PathEmpty>.FromResult(element);
        }
    }
}