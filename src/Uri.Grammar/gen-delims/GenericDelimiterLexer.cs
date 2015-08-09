namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class GenericDelimiterLexer : Lexer<GenericDelimiter>
    {
        private readonly ILexer<Alternative> innerLexer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="innerLexer">":" / "/" / "?" / "#" / "[" / "]" / "@"</param>
        public GenericDelimiterLexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer", "Precondition: innerLexer != null");
            }

            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out GenericDelimiter element)
        {
            Alternative result;
            if (this.innerLexer.TryRead(scanner, null, out result))
            {
                element = new GenericDelimiter(result);
                if (previousElementOrNull != null)
                {
                    previousElementOrNull.NextElement = element;
                    element.PreviousElement = previousElementOrNull;
                }

                return true;
            }

            element = default(GenericDelimiter);
            return false;
        }
    }
}