namespace Uri.Grammar.pchar
{
    using SLANG;

    public class PathCharacterAlternativeLexer : AlternativeLexer
    {
        /// <summary></summary>
        /// <param name="lexers">unreserved / pct-encoded / sub-delims / ":" / "@"</param>
        public PathCharacterAlternativeLexer(params ILexer[] lexers)
            : base(lexers)
        {
        }
    }
}
