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

    public class Query2StringLexer : StringLexer
    {
        public Query2StringLexer()
            : base(@"/")
        {
        }
    }

    public class Query3StringLexer : StringLexer
    {
        public Query3StringLexer()
            : base(@"?")
        {
        }
    }
}
