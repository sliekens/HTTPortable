namespace Uri.Grammar.fragment
{
    using SLANG;

    public class FragmentRepetitionLexer : RepetitionLexer
    {
        /// <summary></summary>
        /// <param name="alternativeLexer">*( pchar / "/" / "?" )</param>
        public FragmentRepetitionLexer(ILexer<Alternative> alternativeLexer)
            : base(alternativeLexer, 0, int.MaxValue)
        {
        }
    }
}
