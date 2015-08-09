namespace Http.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class RequiredDelimitedListLexer : Lexer<RequiredDelimitedList>
    {
        private readonly ILexer<Sequence> innerLexer;

        public RequiredDelimitedListLexer(ILexer<Sequence> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out RequiredDelimitedList element)
        {
            Sequence result;
            if (this.innerLexer.TryRead(scanner, null, out  result))
            {
                element = new RequiredDelimitedList(result);
                if (previousElementOrNull != null)
                {
                    previousElementOrNull.NextElement = element;
                    element.PreviousElement = previousElementOrNull;
                }

                return true;
            }

            element = default(RequiredDelimitedList);
            return false;
        }
    }
}