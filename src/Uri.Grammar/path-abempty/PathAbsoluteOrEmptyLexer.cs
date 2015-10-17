namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class PathAbsoluteOrEmptyLexer : Lexer<PathAbsoluteOrEmpty>
    {
        private readonly ILexer<Repetition> innerLexer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="innerLexer">*( "/" segment )</param>
        public PathAbsoluteOrEmptyLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<PathAbsoluteOrEmpty> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<PathAbsoluteOrEmpty>.FromError(new SyntaxError
                {
                    Message = "Expected 'path-abempty'.",
                    RuleName = "path-abempty",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new PathAbsoluteOrEmpty(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<PathAbsoluteOrEmpty>.FromResult(element);
        }
    }
}