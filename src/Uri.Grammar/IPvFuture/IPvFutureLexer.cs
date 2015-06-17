namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class IPvFutureLexer : Lexer<IPvFuture>
    {
        private readonly ILexer<Sequence> innerLexer;

        public IPvFutureLexer(ILexer<Sequence> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, out IPvFuture element)
        {
            Sequence result;
            if (this.innerLexer.TryRead(scanner, out result))
            {
                element = new IPvFuture(result);
                return true;
            }

            element = default(IPvFuture);
            return false;
        }
    }
}