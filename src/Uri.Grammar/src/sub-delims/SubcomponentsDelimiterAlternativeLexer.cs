namespace Uri.Grammar
{
    using SLANG;

    public class SubcomponentsDelimiterAlternativeLexer : AlternativeLexer
    {
        public SubcomponentsDelimiterAlternativeLexer(params ILexer[] lexers)
            : base(lexers)
        {
        }
    }
}
