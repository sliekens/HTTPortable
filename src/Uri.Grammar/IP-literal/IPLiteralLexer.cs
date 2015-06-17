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

        public override bool TryRead(ITextScanner scanner, out IPLiteral element)
        {
            Sequence result;
            if (this.innerLexer.TryRead(scanner, out result))
            {
                element = new IPLiteral(result);
                return true;
            }

            element = default(IPLiteral);
            return false;
        }
    }
}