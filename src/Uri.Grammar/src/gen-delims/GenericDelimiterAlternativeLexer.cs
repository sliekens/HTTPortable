namespace Uri.Grammar
{
    using SLANG;

    public class GenericDelimiterAlternativeLexer : AlternativeLexer
    {
        /// <summary></summary>
        /// <param name="lexers">":" / "/" / "?" / "#" / "[" / "]" / "@"</param>
        public GenericDelimiterAlternativeLexer(params ILexer[] lexers)
            : base(lexers)
        {
        }
    }
}