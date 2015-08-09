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

        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out IPvFuture element)
        {
            Sequence result;
            if (this.innerLexer.TryRead(scanner, null, out result))
            {
                element = new IPvFuture(result);
                if (previousElementOrNull != null)
                {
                    previousElementOrNull.NextElement = element;
                    element.PreviousElement = previousElementOrNull;
                }

                return true;
            }

            element = default(IPvFuture);
            return false;
        }
    }
}