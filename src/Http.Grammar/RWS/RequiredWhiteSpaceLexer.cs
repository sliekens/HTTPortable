namespace Http.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class RequiredWhiteSpaceLexer : Lexer<RequiredWhiteSpace>
    {
        private readonly ILexer<Repetition> innerLexer;

        /// <summary>
        /// </summary>
        /// <param name="innerLexer">1*( SP / HTAB )</param>
        public RequiredWhiteSpaceLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out RequiredWhiteSpace element)
        {
            Repetition result;
            if (this.innerLexer.TryRead(scanner, null, out result))
            {
                element = new RequiredWhiteSpace(result);
                if (previousElementOrNull != null)
                {
                    previousElementOrNull.NextElement = element;
                    element.PreviousElement = previousElementOrNull;
                }

                return true;
            }

            element = default(RequiredWhiteSpace);
            return false;
        }
    }
}