namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class HexadecimalInt16Lexer : Lexer<HexadecimalInt16>
    {
        private readonly ILexer<Repetition> innerLexer;

        public HexadecimalInt16Lexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, out HexadecimalInt16 element)
        {
            Repetition result;
            if (this.innerLexer.TryRead(scanner, out result))
            {
                element = new HexadecimalInt16(result);
                return true;
            }

            element = default(HexadecimalInt16);
            return false;
        }
    }
}