namespace Http.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class OptionalWhiteSpaceLexer : Lexer<OptionalWhiteSpace>
    {
        private readonly ILexer<Repetition> innerLexer;

        /// <summary>
        /// </summary>
        /// <param name="innerLexer">*( SP / HTAB )</param>
        public OptionalWhiteSpaceLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }
            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out OptionalWhiteSpace element)
        {
            Repetition result;
            if (this.innerLexer.TryRead(scanner, null, out result))
            {
                element = new OptionalWhiteSpace(result);
                if (previousElementOrNull != null)
                {
                    previousElementOrNull.NextElement = element;
                    element.PreviousElement = previousElementOrNull;
                }

                return true;
            }

            element = default(OptionalWhiteSpace);
            return false;
        }
    }
}