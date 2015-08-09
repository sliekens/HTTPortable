namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class FragmentLexer : Lexer<Fragment>
    {
        private readonly ILexer<Repetition> innerLexer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="innerLexer">*( pchar / "/" / "?" )</param>
        public FragmentLexer(ILexer<Repetition> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer", "Precondition: innerLexer != null");
            }

            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out Fragment element)
        {
            Repetition result;
            if (this.innerLexer.TryRead(scanner, null, out result))
            {
                element = new Fragment(result);
                if (previousElementOrNull != null)
                {
                    previousElementOrNull.NextElement = element;
                    element.PreviousElement = previousElementOrNull;
                }

                return true;
            }

            element = default(Fragment);
            return false;
        }
    }
}