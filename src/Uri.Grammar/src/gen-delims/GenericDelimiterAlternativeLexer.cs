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

    public class GenericDelimiter1StringLexer : StringLexer
    {
        public GenericDelimiter1StringLexer()
            : base(@":")
        {
        }
    }

    public class GenericDelimiter2StringLexer : StringLexer
    {
        public GenericDelimiter2StringLexer()
            : base(@"/")
        {
        }
    }

    public class GenericDelimiter3StringLexer : StringLexer
    {
        public GenericDelimiter3StringLexer()
            : base(@"?")
        {
        }
    }

    public class GenericDelimiter4StringLexer : StringLexer
    {
        public GenericDelimiter4StringLexer()
            : base(@"#")
        {
        }
    }

    public class GenericDelimiter5StringLexer : StringLexer
    {
        public GenericDelimiter5StringLexer()
            : base(@"[")
        {
        }
    }

    public class GenericDelimiter6StringLexer : StringLexer
    {
        public GenericDelimiter6StringLexer()
            : base(@"]")
        {
        }
    }

    public class GenericDelimiter7StringLexer : StringLexer
    {
        public GenericDelimiter7StringLexer()
            : base(@"@")
        {
        }
    }
}