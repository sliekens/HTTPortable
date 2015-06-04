namespace Uri.Grammar
{
    using System;

    using SLANG;

    public class PercentEncodingLexer : Lexer<PercentEncoding>
    {
        private readonly ILexer<Sequence> percentEncodingAlternativeLexer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="percentEncodingAlternativeLexer">"%" HEXDIG HEXDIG</param>
        public PercentEncodingLexer(ILexer<Sequence> percentEncodingAlternativeLexer)
        {
            if (percentEncodingAlternativeLexer == null)
            {
                throw new ArgumentNullException("percentEncodingAlternativeLexer", "Precondition: percentEncodingAlternativeLexer != null");
            }

            this.percentEncodingAlternativeLexer = percentEncodingAlternativeLexer;
        }

        public override bool TryRead(ITextScanner scanner, out PercentEncoding element)
        {
            Sequence result;
            if (this.percentEncodingAlternativeLexer.TryRead(scanner, out result))
            {
                element = new PercentEncoding(result);
                return true;
            }

            element = default(PercentEncoding);
            return false;
        }
    }
}