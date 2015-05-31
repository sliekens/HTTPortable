namespace Uri.Grammar.query
{
    using SLANG;

    public class QueryRepetitionLexer : RepetitionLexer
    {
        /// <summary></summary>
        /// <param name="alternativeLexer">*( pchar / "/" / "?" )</param>
        public QueryRepetitionLexer(ILexer<Alternative> alternativeLexer)
            : base(alternativeLexer, 0, int.MaxValue)
        {
        }
    }
}
