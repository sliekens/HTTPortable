namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class PercentEncodingLexer : Lexer<PercentEncoding>
    {
        private readonly ILexer<Sequence> innerLexer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="innerLexer">"%" HEXDIG HEXDIG</param>
        public PercentEncodingLexer(ILexer<Sequence> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer", "Precondition: innerLexer != null");
            }

            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out PercentEncoding element)
        {
            Sequence result;
            if (this.innerLexer.TryRead(scanner, null, out result))
            {
                element = new PercentEncoding(result);
                if (previousElementOrNull != null)
                {
                    previousElementOrNull.NextElement = element;
                    element.PreviousElement = previousElementOrNull;
                }

                return true;
            }

            element = default(PercentEncoding);
            return false;
        }
    }
}