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

    public class Fragment2StringLexer : StringLexer
    {
        public Fragment2StringLexer()
            : base(@"/")
        {
        }
    }

    public class Fragment3StringLexer : StringLexer
    {
        public Fragment3StringLexer()
            : base(@"?")
        {
        }
    }
}
