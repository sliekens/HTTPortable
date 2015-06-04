namespace Uri.Grammar
{
    using System;

    using SLANG;

    public class GenericDelimiterLexer : Lexer<GenericDelimiter>
    {
        private readonly ILexer<Alternative> genericDelimiterAlternativeLexer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="genericDelimiterAlternativeLexer">":" / "/" / "?" / "#" / "[" / "]" / "@"</param>
        public GenericDelimiterLexer(ILexer<Alternative> genericDelimiterAlternativeLexer)
            : base("gen-delims")
        {
            if (genericDelimiterAlternativeLexer == null)
            {
                throw new ArgumentNullException("genericDelimiterAlternativeLexer", "Precondition: genericDelimiterAlternativeLexer != null");
            }

            this.genericDelimiterAlternativeLexer = genericDelimiterAlternativeLexer;
        }

        public override bool TryRead(ITextScanner scanner, out GenericDelimiter element)
        {
            Alternative result;
            if (this.genericDelimiterAlternativeLexer.TryRead(scanner, out result))
            {
                element = new GenericDelimiter(result);
                return true;
            }

            element = default(GenericDelimiter);
            return false;
        }
    }
}