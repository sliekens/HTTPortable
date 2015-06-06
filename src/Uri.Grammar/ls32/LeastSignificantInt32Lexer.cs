namespace Uri.Grammar
{
    using System;

    using SLANG;

    public class LeastSignificantInt32Lexer : Lexer<LeastSignificantInt32>
    {
        private readonly ILexer<Alternative> innerLexer;

        public LeastSignificantInt32Lexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, out LeastSignificantInt32 element)
        {
            Alternative result;
            if (this.innerLexer.TryRead(scanner, out result))
            {
                element = new LeastSignificantInt32(result);
                return true;
            }

            element = default(LeastSignificantInt32);
            return false;
        }
    }
}