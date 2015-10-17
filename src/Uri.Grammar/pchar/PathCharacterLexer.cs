namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class PathCharacterLexer : Lexer<PathCharacter>
    {
        private readonly ILexer<Alternative> innerLexer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="innerLexer">unreserved / pct-encoded / sub-delims / ":" / "@"</param>
        public PathCharacterLexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer", "Precondition: innerLexer != null");
            }

            this.innerLexer = innerLexer;
        }

        public override ReadResult<PathCharacter> Read(ITextScanner scanner, Element previousElementOrNull)
        {
            var result = this.innerLexer.Read(scanner, null);
            if (!result.Success)
            {
                return ReadResult<PathCharacter>.FromError(new SyntaxError
                {
                    Message = "Expected 'pchar'.",
                    RuleName = "pchar",
                    Context = scanner.GetContext(),
                    InnerError = result.Error
                });
            }

            var element = new PathCharacter(result.Element);
            if (previousElementOrNull != null)
            {
                previousElementOrNull.NextElement = element;
                element.PreviousElement = previousElementOrNull;
            }

            return ReadResult<PathCharacter>.FromResult(element);
        }
    }
}