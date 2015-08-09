namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

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

        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out LeastSignificantInt32 element)
        {
            Alternative result;
            if (this.innerLexer.TryRead(scanner, null, out result))
            {
                element = new LeastSignificantInt32(result);
                if (previousElementOrNull != null)
                {
                    previousElementOrNull.NextElement = element;
                    element.PreviousElement = previousElementOrNull;
                }

                return true;
            }

            element = default(LeastSignificantInt32);
            return false;
        }
    }
}