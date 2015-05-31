namespace Uri.Grammar.reserved
{
    using SLANG;

    public class ReservedAlternativeLexer : AlternativeLexer
    {
        /// <summary></summary>
        /// <param name="lexers">gen-delims / sub-delims</param>
        public ReservedAlternativeLexer(params ILexer[] lexers)
            : base(lexers)
        {
        }
    }
}
