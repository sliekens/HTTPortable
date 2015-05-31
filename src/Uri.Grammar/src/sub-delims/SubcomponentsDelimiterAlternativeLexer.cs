namespace Uri.Grammar
{
    using SLANG;

    public class SubcomponentsDelimiterAlternativeLexer : AlternativeLexer
    {
        /// <summary></summary>
        /// <param name="lexers">"!" / "$" / "&" / "'" / "(" / ")" / "*" / "+" / "," / ";" / "="</param>
        public SubcomponentsDelimiterAlternativeLexer(params ILexer[] lexers)
            : base(lexers)
        {
        }
    }
}
