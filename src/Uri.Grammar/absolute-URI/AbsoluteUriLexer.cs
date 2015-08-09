namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class AbsoluteUriLexer : Lexer<AbsoluteUri>
    {
        private readonly ILexer<Sequence> innerLexer;

        public AbsoluteUriLexer(ILexer<Sequence> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out AbsoluteUri element)
        {
            Sequence result;
            if (this.innerLexer.TryRead(scanner, null, out result))
            {
                element = new AbsoluteUri(result);
                if (previousElementOrNull != null)
                {
                    previousElementOrNull.NextElement = element;
                    element.PreviousElement = previousElementOrNull;
                }

                return true;
            }

            element = default(AbsoluteUri);
            return false;
        }
    }
}