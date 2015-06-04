namespace Uri.Grammar.query
{
    using SLANG;

    public class QueryAlternativeLexer : AlternativeLexer
    {
        /// <summary></summary>
        /// <param name="lexers">pchar / "/" / "?"</param>
        public QueryAlternativeLexer(params ILexer[] lexers)
            : base(lexers)
        {
        }
    }
}
