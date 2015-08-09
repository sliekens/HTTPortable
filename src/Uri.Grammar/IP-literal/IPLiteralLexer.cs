namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class IPLiteralLexer : Lexer<IPLiteral>
    {
        private readonly ILexer<Sequence> innerLexer;

        public IPLiteralLexer(ILexer<Sequence> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out IPLiteral element)
        {
            Sequence result;
            if (this.innerLexer.TryRead(scanner, null, out result))
            {
                element = new IPLiteral(result);
                if (previousElementOrNull != null)
                {
                    previousElementOrNull.NextElement = element;
                    element.PreviousElement = previousElementOrNull;
                }

                return true;
            }

            element = default(IPLiteral);
            return false;
        }
    }
}