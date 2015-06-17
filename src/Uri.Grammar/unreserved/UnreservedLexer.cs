namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class UnreservedLexer : Lexer<Unreserved>
    {
        private readonly ILexer<Alternative> unreservedAlternativeLexer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unreservedAlternativeLexer">ALPHA / DIGIT / "-" / "." / "_" / "~"</param>
        public UnreservedLexer(ILexer<Alternative> unreservedAlternativeLexer)
        {
            if (unreservedAlternativeLexer == null)
            {
                throw new ArgumentNullException("unreservedAlternativeLexer", "Precondition: unreservedAlternativeLexer != null");
            }

            this.unreservedAlternativeLexer = unreservedAlternativeLexer;
        }

        public override bool TryRead(ITextScanner scanner, out Unreserved element)
        {
            Alternative result;
            if (this.unreservedAlternativeLexer.TryRead(scanner, out result))
            {
                element = new Unreserved(result);
                return true;
            }

            element = default(Unreserved);
            return false;
        }
    }
}