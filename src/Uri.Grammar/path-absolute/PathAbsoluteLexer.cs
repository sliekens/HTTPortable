namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class PathAbsoluteLexer : Lexer<PathAbsolute>
    {
        private readonly ILexer<Sequence> innerLexer;

        /// <summary>
        /// </summary>
        /// <param name="innerLexer">"/" [ segment-nz *( "/" segment ) ]</param>
        public PathAbsoluteLexer(ILexer<Sequence> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<PathAbsolute> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<PathAbsolute>.FromError(new SyntaxError
                {
                    Message = "Expected 'path-absolute'.",
                    RuleName = "path-absolute",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new PathAbsolute(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<PathAbsolute>.FromResult(element);
        }
    }
}