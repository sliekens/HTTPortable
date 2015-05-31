namespace Uri.Grammar
{
    using SLANG;

    public class PercentEncodingSequenceLexer : SequenceLexer
    {
        /// <summary></summary>
        /// <param name="lexers">"%" HEXDIG HEXDIG</param>
        public PercentEncodingSequenceLexer(params ILexer[] lexers)
            : base(lexers)
        {
        }
    }

    public class PercentEncoding1StringLexer : StringLexer
    {
        public PercentEncoding1StringLexer()
            : base(@"%")
        {
        }
    }
}
