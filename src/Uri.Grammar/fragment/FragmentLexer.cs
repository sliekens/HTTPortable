namespace Uri.Grammar
{
    using System;

    using SLANG;

    public class FragmentLexer : Lexer<Fragment>
    {
        private readonly ILexer<Repetition> fragmentRepetitionLexer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fragmentRepetitionLexer">*( pchar / "/" / "?" )</param>
        public FragmentLexer(ILexer<Repetition> fragmentRepetitionLexer)
        {
            if (fragmentRepetitionLexer == null)
            {
                throw new ArgumentNullException("fragmentRepetitionLexer", "Precondition: fragmentRepetitionLexer != null");
            }

            this.fragmentRepetitionLexer = fragmentRepetitionLexer;
        }

        public override bool TryRead(ITextScanner scanner, out Fragment element)
        {
            Repetition result;
            if (this.fragmentRepetitionLexer.TryRead(scanner, out result))
            {
                element = new Fragment(result);
                return true;
            }

            element = default(Fragment);
            return false;
        }
    }
}