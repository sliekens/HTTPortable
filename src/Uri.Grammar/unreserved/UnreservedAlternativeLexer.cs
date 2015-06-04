namespace Uri.Grammar.unreserved
{
    using SLANG;

    public class UnreservedAlternativeLexer : AlternativeLexer
    {
        /// <summary></summary>
        /// <param name="lexers">ALPHA / DIGIT / "-" / "." / "_" / "~"</param>
        public UnreservedAlternativeLexer(params ILexer[] lexers)
            : base(lexers)
        {
        }
    }
}
