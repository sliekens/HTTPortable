namespace Uri.Grammar.fragment
{
    using SLANG;

    public class FragmentAlternativeLexer : AlternativeLexer
    {
        /// <summary></summary>
        /// <param name="lexers">pchar / "/" / "?"</param>
        public FragmentAlternativeLexer(params ILexer[] lexers)
            : base(lexers)
        {
        }
    }
}
